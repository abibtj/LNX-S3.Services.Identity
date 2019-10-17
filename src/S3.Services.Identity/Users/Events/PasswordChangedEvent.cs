using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class PasswordChangedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public PasswordChangedEvent(Guid userId) => UserId = userId;
    }
}