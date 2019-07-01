using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class SignedInEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public SignedInEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}