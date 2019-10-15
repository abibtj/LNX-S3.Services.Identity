using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Commands
{
    public class UpdateRolesCommand : ICommand
    {
        public Guid UserId { get; }
        public string[] Roles { get; }

        [JsonConstructor]
        public UpdateRolesCommand(Guid userId, string[] roles)
            => (UserId, Roles) = (userId, roles);
    }
}