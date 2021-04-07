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
using Microsoft.Extensions.Configuration;

namespace S3.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityController(IIdentityService identityService,
            IRefreshTokenService refreshTokenService, IConfiguration configuration)
            : base(configuration)

            => (_identityService, _refreshTokenService) = (identityService, refreshTokenService);


        [HttpGet("check-username")]
        public async Task<bool> CheckUsernameAvailability(string username) 
            => await _identityService.CheckUsernameAvailabilityAsync(username);
      
        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        //[JwtAuth(Roles = "Super Admin, Admin, School Admin, Assistant School Admin")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            // Superadmin can sign up users with any role
            // Check the role(s) of the SignUpCommand sender and ensure they
            // can sign up a user with the role(s) specified in the command

            if (!IsSuperAdmin)
                CheckUserCanAssignRoles(command.Roles);

            await _identityService.SignUpAsync(command.UserId, command.SchoolId, 
                command.Username, command.Password, command.Roles);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
            => Ok(await _identityService.SignInAsync(command.Username, command.Password));

        [HttpPut("change-password")]
        [JwtAuth]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _identityService.ChangePasswordAsync(command.Bind(c => c.UserId, UserId).UserId, 
                command.CurrentPassword, command.NewPassword);

            return NoContent();
        }

        [HttpPut("reset-password")]
        [JwtAuth(Roles = "Super Admin, Admin, School Admin, Assistant School Admin")]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await _identityService.ResetPasswordAsync(command.UserId, command.Password);
            //await _identityService.ResetPasswordAsync(command.Bind(c => c.UserId, UserId).UserId, command.Password);

            return NoContent();
        }

        [HttpDelete("remove-sign-up/{id:guid}")]
        [JwtAuth(Roles = "Super Admin, Admin, School Admin, Assistant School Admin")]
        public async Task<ActionResult> RemoveSignUp(Guid id)
        {
            await _identityService.RemoveSignUpAsync(id);

            return NoContent();
        }

        [HttpPut("update-roles")]
        [JwtAuth(Roles = "Super Admin, Admin, School Admin, Assistant School Admin")]
        public async Task<ActionResult> UpdateRoles(UpdateRolesCommand command)
        {
            await _identityService.UpdateUserRolesAsync(command.UserId, command.Roles);

            return NoContent();
        }
    }
}