using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class SignUpCommand
    {
        [Required]
        public Guid UserId { get; }
        [Required]
        public Guid SchoolId { get; }
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; }
        [Required(ErrorMessage ="A minimum of one role is required.")]
        public string[] Roles { get; }

        [JsonConstructor]
        public SignUpCommand(Guid userId, Guid schoolId, string username, string password, string[] roles)
            => (UserId, SchoolId, Username, Password, Roles) 
            = (userId, schoolId, username, password, roles);
        //public SignUpCommand(Guid schoolId, string username, string password, string[] roles)
        //    => (Id, SchoolId, Username, Password, Roles) 
        //    = (Guid.NewGuid(), schoolId, username, password, roles);
    }
}