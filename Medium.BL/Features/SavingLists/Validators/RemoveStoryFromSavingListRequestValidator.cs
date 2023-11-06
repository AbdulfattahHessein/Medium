using FluentValidation;
using Medium.BL.Features.SavingLists.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.SavingLists.Validators
{
    internal class RemoveStoryFromSavingListRequestValidator : AbstractValidator<RemoveStoryFromSavingListRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveStoryFromSavingListRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

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
