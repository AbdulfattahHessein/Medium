using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class UpdateSavingListValidator : AbstractValidator<UpdateSavingListRequest>
    {
        public UpdateSavingListValidator()
        {
            RuleFor(s => s.Id)
                .NotNull().WithMessage("{PropertyName}Must be not null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(s => s.Name)
    .NotNull().WithMessage("{PropertyName}Must be not null")
    .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(s => s.PublisherId)
    .NotNull().WithMessage("{PropertyName}Must be not null")
    .NotEmpty().WithMessage("{PropertyName}Must be not empty");
        }
    }
}
