using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class SignInCommand : ICommand
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}