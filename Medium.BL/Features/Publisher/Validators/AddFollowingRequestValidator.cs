using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class AddFollowingRequestValidator : AbstractValidator<AddFollowingRequest>
    {
        public AddFollowingRequestValidator()
        {
            RuleFor(p => p.PublisherId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(p => p.FollowingId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(p => p.FollowingId)
                .MustAsync(async (af, i, c) => await Task.Run(() => af.FollowingId != af.PublisherId))
                .WithMessage("Publisher can't follow itself");
        }
    }
}
