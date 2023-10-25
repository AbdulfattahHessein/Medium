using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class CreateSavingListValidator : AbstractValidator<CreateSavingListRequest>
    {
        public CreateSavingListValidator()
        {
            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

        }
    }
}
