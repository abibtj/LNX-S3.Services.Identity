using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Identity.Users.Events
{
    public class SignInRejectedEvent : IRejectedEvent
    {
        public string Username { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public SignInRejectedEvent(string username, string reason, string code)
        {
            Username = username;
            Reason = reason;
            Code = code;
        }
    }
}