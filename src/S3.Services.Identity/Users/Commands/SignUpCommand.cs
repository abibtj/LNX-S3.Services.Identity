using System;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class SignUpCommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignUpCommand(string email, string password, string role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Role = role;
        }
    }
}