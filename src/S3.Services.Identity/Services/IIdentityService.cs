using System;
using System.Threading.Tasks;
using S3.Common.Authentication;
using S3.Services.Identity.Domain;

namespace S3.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, Guid schoolId, string username, string password, string[] roles);
        Task<JsonWebToken> SignInAsync(string username, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);         
        Task ResetPasswordAsync(Guid userId, string newPassword);         
        Task RemoveSignUpAsync(Guid userId);         
        Task UpdateUserRolesAsync(Guid userId, string[] roles);         
        Task<bool> CheckUsernameAvailabilityAsync(string username);         
    }
}