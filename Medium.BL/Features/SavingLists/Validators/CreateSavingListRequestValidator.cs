using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class CreateSavingListRequestValidator : AbstractValidator<CreateSavingListRequest>
    {
        public CreateSavingListRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");

            RuleFor(p => p.PublisherId)
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");


        }
    }
}
