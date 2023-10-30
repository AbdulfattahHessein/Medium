using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class DeleteSavingListRequestValidator : AbstractValidator<DeleteSavingListRequest>
    {
        public DeleteSavingListRequestValidator()
        {
            RuleFor(t => t.Id).NotEmpty().WithMessage("{PropertyName} must be not empty")
                .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
