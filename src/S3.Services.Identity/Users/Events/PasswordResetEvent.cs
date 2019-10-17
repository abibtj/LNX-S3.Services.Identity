using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class PasswordResetEvent : IEvent
    {
        public Guid UserId { get; }
        public string NewPassword { get; }

        [JsonConstructor]
        public PasswordResetEvent(Guid userId, string newPassword)
            => (UserId, NewPassword) = (userId, newPassword);
    }
}