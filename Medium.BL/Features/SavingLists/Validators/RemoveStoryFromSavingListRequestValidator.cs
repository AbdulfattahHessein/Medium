using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    internal class RemoveStoryFromSavingListRequestValidator : AbstractValidator<RemoveStoryFromSavingListRequest>
    {
        public RemoveStoryFromSavingListRequestValidator()
        {
            RuleFor(p => p.SavingListId).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

            RuleFor(p => p.StoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

        }
    }
}
