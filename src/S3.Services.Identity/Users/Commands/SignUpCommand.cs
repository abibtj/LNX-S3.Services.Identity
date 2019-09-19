using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class SignUpCommand
    {
        public Guid Id { get; }
        [Required]
        public Guid SchoolId { get; }
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; }
        [Required(ErrorMessage ="Role is required.")]
        public string Role { get; }

        [JsonConstructor]
        public SignUpCommand(Guid schoolId, string username, string password, string role)
            => (Id, SchoolId, Username, Password, Role) 
            = (Guid.NewGuid(), schoolId, username, password, role);
    }
}