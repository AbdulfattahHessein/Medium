using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class AddStoryToSaveListRequestValidator : AbstractValidator<AddStoryToSaveListRequest>
    {
        public AddStoryToSaveListRequestValidator()
        {
            RuleFor(p => p.SaveListId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

            RuleFor(p => p.StoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");
        }
    }
}
