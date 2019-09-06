﻿using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using S3.Common;
using S3.Common.Types;


namespace S3.Services.Identity.Domain
{
    public class User : IIdentifiable
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public Guid Id { get; private set; }
        //public string Email { get; private set; }
        public string Username { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private User()
        {

        }

        public User(Guid id, string username, string role)
        {
            //if (!EmailRegex.IsMatch(username))
            //{
            //    throw new S3Exception(ExceptionCodes.InvalidEmail,
            //        $"Invalid email: '{username}'.");
            //}
            if (!Domain.Role.IsValid(role))
            {
                throw new S3Exception(ExceptionCodes.InvalidRole,
                    $"Invalid role: '{role}'.");
            }

            Id = id;
            Username = username;
            Role = role.ToLowerInvariant();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            ValidatePassword(password);
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool VerifyHashedPassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                    "Password cannot be empty.");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                     "Password must contain at least one lower case letter.");
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                    "Password must contain at least one upper case letter.");
            }
            else if (!hasMiniMaxChars.IsMatch(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                    "Password must be between 6 and 20 characters long.");
            }
            else if (!hasNumber.IsMatch(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                     "Password must contain at least one digit [0 - 9].");
            }

            else if (!hasSymbols.IsMatch(password))
            {
                throw new S3Exception(ExceptionCodes.InvalidPassword,
                    "Password must contain at least one symbol.");
            }
            else
            {
                return true;
            }
        }
    }
}
