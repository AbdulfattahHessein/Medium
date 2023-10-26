using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class UpdatePublisherRequestValidator : AbstractValidator<UpdatePublisherRequest>
    {
        public UpdatePublisherRequestValidator()
        {
            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(p => p.Bio).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");

        }

    }
}
