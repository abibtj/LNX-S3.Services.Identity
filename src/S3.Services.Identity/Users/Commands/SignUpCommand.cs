using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class SignUpCommand
    {
        public Guid Id { get; }
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; }
        [Required(ErrorMessage ="Role is required.")]
        public string Role { get; }

        [JsonConstructor]
        public SignUpCommand(string username, string password, string role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Role = role;
        }
    }
}