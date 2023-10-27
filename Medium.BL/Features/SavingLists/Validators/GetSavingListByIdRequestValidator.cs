using FluentValidation;
using Medium.BL.Features.SavingLists.Request;

namespace Medium.BL.Features.SavingLists.Validators
{
    internal class GetSavingListByIdRequestValidator : AbstractValidator<GetSavingListByIdRequest>
    {
        public GetSavingListByIdRequestValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

        }
    }
}
