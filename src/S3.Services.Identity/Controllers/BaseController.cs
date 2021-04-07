using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using S3.Common.Types;

namespace S3.Services.Identity.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
            => _configuration = configuration;

         protected bool IsSuperAdmin
            => User.IsInRole("Super Admin");

        protected bool IsAdmin
            => User.IsInRole("Admin");

         protected bool IsSchoolAdmin
            => User.IsInRole("School Admin");
         protected bool IsAssistantSchoolAdmin
            => User.IsInRole("Assistant School Admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ? 
                Guid.Empty : 
                Guid.Parse(User.Identity.Name);

        protected void CheckUserCanAssignRoles(string[] roles)
        {
            Roles _roles = new Roles();
            
            if (IsAdmin)
            {
               _configuration.GetSection("roles:admin").Bind(_roles);
            }
            else if (IsSchoolAdmin)
            {
                _configuration.GetSection("roles:schooladmin").Bind(_roles);
            }
            else if (IsAssistantSchoolAdmin)
            {
               _configuration.GetSection("roles:assistantSchooladmin").Bind(_roles);
            }
            else
            {
               _configuration.GetSection("roles:default").Bind(_roles);
            }

            if (roles.Except(_roles.AssignableRoles).Any())
                throw new S3Exception("Unathorised_Role", 
                    "You are not authorised to sign up with the specified role(s).");
        }

        class Roles
        {
            public string[] AssignableRoles { get; set; }
        }
    }
}