using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class SignedUpEvent : IEvent
    {
        public Guid UserId { get; }
        public string Username { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignedUpEvent(Guid userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }
    }
}