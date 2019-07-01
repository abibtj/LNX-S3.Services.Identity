using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class RevokeRefreshTokenCommand : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        [JsonConstructor]
        public RevokeRefreshTokenCommand(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}