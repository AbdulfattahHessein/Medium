using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class FollowerNotFollowingRequestValidator : AbstractValidator<FollowerNotFollowingRequest>
    {
        public FollowerNotFollowingRequestValidator()
        {
            RuleFor(p => p.PublisherId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(p => p.PageNumber).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(p => p.PageSize).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }
}
