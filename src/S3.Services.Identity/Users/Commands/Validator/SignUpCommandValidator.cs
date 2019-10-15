
using FluentValidation;
using System;

namespace S3.Services.Identity.Users.Commands.Validators
{
    public class SignUpCommandCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandCommandValidator()
        {
            RuleFor(x => x.UserId)
                  .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.SchoolId)
                  .NotEmpty().WithMessage("School's Id is required.");
        }
    }
}
