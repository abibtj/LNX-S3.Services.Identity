using System;
using System.Threading.Tasks;
using S3.Common.Authentication;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Identity.Users.Events;
using S3.Services.Identity.Domain;
using S3.Services.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using S3.Common;
using System.Linq;
using S3.Common.Utility;

namespace S3.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClaimsProvider _claimsProvider;
        private readonly IBusPublisher _busPublisher;

        public IdentityService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IRefreshTokenRepository refreshTokenRepository,
            IClaimsProvider claimsProvider,
            IBusPublisher busPublisher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsProvider = claimsProvider;
            _busPublisher = busPublisher;
        }

        public async Task SignUpAsync(Guid id, Guid schoolId, string username, string password, string[] roles)
        {
            var user = await _userRepository.GetAsync(username);
            if (user != null)
            {
                throw new S3Exception(ExceptionCodes.UsernameInUse,
                    $"Username: '{username}' is already in use.");
            }

            // Trying to create a user with role SchoolAdmin? check if a school admin is not already existing for this school
            if (roles.ToList().Any(x => x.ToLowerInvariant() == Role.SchoolAdmin.ToLowerInvariant()))
            {
                if (await _userRepository.SchoolAdminExistsAsync(schoolId))
                {
                    throw new S3Exception(ExceptionCodes.SchoolAdminExists,
                       $"School Admin already exists for school with id: {schoolId}.");
                }
            }
           
            user = new User(id, schoolId, username, roles);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _busPublisher.PublishAsync(new SignedUpEvent(id, roles), CorrelationContext.Empty);
        }

        public async Task<JsonWebToken> SignInAsync(string username, string password)
        {
            var user = await _userRepository.GetAsync(username);
            if (user == null || !user.VerifyHashedPassword(password, _passwordHasher))
            {
                throw new S3Exception(ExceptionCodes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProvider.GetAsync(user);
            //var claims = await _claimsProvider.GetAsync(user.Id);
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Roles, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new S3Exception(ExceptionCodes.NotFound, 
                    $"User with id: '{userId}' was not found.");
            }
            if (!user.VerifyHashedPassword(currentPassword, _passwordHasher))
            {
                throw new S3Exception(ExceptionCodes.InvalidCurrentPassword, 
                    "Invalid current password.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);
            await _busPublisher.PublishAsync(new PasswordChangedEvent(userId), CorrelationContext.Empty);         
        }

        public async Task ResetPasswordAsync(Guid userId, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new S3Exception(ExceptionCodes.NotFound, 
                    $"User with id: '{userId}' was not found.");
            }
           
            user.SetPassword(newPassword, _passwordHasher);
            user.SetUpdatedDate();
            await _userRepository.UpdateAsync(user);
            await _busPublisher.PublishAsync(new PasswordResetEvent(userId, newPassword), CorrelationContext.Empty);
        }

        public async Task RemoveSignUpAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new S3Exception(ExceptionCodes.NotFound, 
                    $"User with id: '{userId}' was not found.");
            }
           
            await _userRepository.RemoveAsync(userId);
            await _busPublisher.PublishAsync(new SignUpRemovedEvent(user.Id, user.Roles), CorrelationContext.Empty);
        }

        public async Task UpdateUserRolesAsync(Guid userId, string[] roles)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"User with id: '{userId}' was not found.");
            }

            user.SetRoles(roles);
            user.SetUpdatedDate();
            await _userRepository.UpdateAsync(user);
            await _busPublisher.PublishAsync(new UserRolesUpdatedEvent(user.Id, user.Roles), CorrelationContext.Empty);
        }

        public async Task<bool> CheckUsernameAvailabilityAsync(string username)
        {
            var user = await _userRepository.GetAsync(username);
            return (user is null) ? true : false;
        }
    }
}