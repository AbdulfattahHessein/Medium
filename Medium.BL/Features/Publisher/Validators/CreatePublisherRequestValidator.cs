using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class CreatePublisherRequestValidator : AbstractValidator<CreatePublisherRequest>
    {
        public CreatePublisherRequestValidator()
        {
            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(p => p.Bio).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");

            //RuleFor(p => p.Photo).NotNull().
            //    WithMessage("You Must Upload Your Photo");
        }
    }
}
