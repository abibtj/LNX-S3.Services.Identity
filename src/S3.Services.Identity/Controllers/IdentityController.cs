using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using S3.Common;
using S3.Common.Mvc;
using S3.Common.Types;
using S3.Services.Identity.Services;
using System;
using S3.Common.Authentication;
using S3.Services.Identity.Users.Commands;
using S3.Services.Identity.Domain;

namespace S3.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityController(IIdentityService identityService,
            IRefreshTokenService refreshTokenService)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            command.BindId(c => c.Id);
            await _identityService.SignUpAsync(command.Id, command.SchoolId, 
                command.Username, command.Password, command.Role);

            return NoContent();
        }

        [HttpPost("sign-up-teacher-parent")]
        [JwtAuth(Roles ="superadmin,admin")]
        public async Task<IActionResult> SignUpTeacherParent(SignUpCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Only authorise creation of users with "teacher" or "parent" roles.
            var role = command.Role.Trim().ToLowerInvariant();
            if (role != Role.Teacher && role != Role.Parent)
                throw new S3Exception(ExceptionCodes.Unauthorized,
                      $"You are not authorised to create a user with Role: {command.Role}.");

            command.BindId(c => c.Id);
            await _identityService.SignUpAsync(command.Id, command.SchoolId,
                command.Username, command.Password, command.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
            => Ok(await _identityService.SignInAsync(command.Username, command.Password));

        [HttpPut("me/password")]
        [JwtAuth]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _identityService.ChangePasswordAsync(command.Bind(c => c.UserId, UserId).UserId, 
                command.CurrentPassword, command.NewPassword);

            return NoContent();
        }
    }
}