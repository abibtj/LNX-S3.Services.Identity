using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class AccessTokenRevokedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public AccessTokenRevokedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}