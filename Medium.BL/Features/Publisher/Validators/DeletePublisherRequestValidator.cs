using FluentValidation;
using Medium.BL.Features.Publisher.Requests;

namespace Medium.BL.Features.Publisher.Validators
{
    public class DeletePublisherRequestValidator : AbstractValidator<DeletePublisherRequest>
    {
        public DeletePublisherRequestValidator()
        {
            RuleFor(p => p.Id).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }

}
