using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class AccessTokenRefreshedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public AccessTokenRefreshedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}