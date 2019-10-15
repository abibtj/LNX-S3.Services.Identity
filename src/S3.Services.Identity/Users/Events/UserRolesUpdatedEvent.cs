using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class UserRolesUpdatedEvent : IEvent
    {
        public Guid UserId { get; }
        public string[] Roles { get; }

        [JsonConstructor]
        public UserRolesUpdatedEvent(Guid userId, string[] roles)
            => (UserId, Roles) = (userId, roles);
    }
}