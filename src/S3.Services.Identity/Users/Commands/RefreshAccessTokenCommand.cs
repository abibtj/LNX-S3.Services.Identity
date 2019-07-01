using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class RefreshAccessTokenCommand : ICommand
    {
        public string Token { get; }

        [JsonConstructor]
        public RefreshAccessTokenCommand(string token)
        {
            Token = token;
        }
    }
}