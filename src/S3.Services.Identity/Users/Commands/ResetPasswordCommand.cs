using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class ResetPasswordCommand : ICommand
    {
        public Guid UserId { get; }
        public string Password { get; }

        [JsonConstructor]
        public ResetPasswordCommand(Guid userId, string password)
            => (UserId, Password) = (userId, password);
    }
}