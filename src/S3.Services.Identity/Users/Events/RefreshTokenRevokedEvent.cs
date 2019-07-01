using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class RefreshTokenRevokedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public RefreshTokenRevokedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}