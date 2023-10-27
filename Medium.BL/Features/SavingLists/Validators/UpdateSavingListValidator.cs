using FluentValidation;
using Medium.BL.Features.SavingLists.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.SavingLists.Validators
{
    public class UpdateSavingListValidator : AbstractValidator<UpdateSavingListRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSavingListValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName}Must be not null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty")
                .MustAsync(async (ur, i, c) =>
                {
                    return await _unitOfWork.SavingLists.AnyAsync(r => r.Id == ur.Id);
                })
                .WithMessage("Publisher is not found"); ;

            RuleFor(s => s.Name)
    .NotNull().WithMessage("{PropertyName}Must be not null")
    .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(s => s.PublisherId)
    .NotNull().WithMessage("{PropertyName}Must be not null")
    .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            _unitOfWork = unitOfWork;
        }
    }
}
