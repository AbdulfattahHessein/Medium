using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class DeleteFollowingRequestValidator : AbstractValidator<DeleteFollowingRequest>
    {
        public DeleteFollowingRequestValidator()
        {
            RuleFor(p => p.PublisherId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(p => p.FollowingId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }
}
