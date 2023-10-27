using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class DeleteStoryRequestValidator : AbstractValidator<DeleteStoryRequest>
    {
        public DeleteStoryRequestValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

        }
    }
}
