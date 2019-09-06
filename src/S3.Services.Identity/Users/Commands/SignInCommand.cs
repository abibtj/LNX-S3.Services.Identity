using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Identity.Users.Commands
{
    public class SignInCommand : ICommand
    {
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; }

        [JsonConstructor]
        public SignInCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}