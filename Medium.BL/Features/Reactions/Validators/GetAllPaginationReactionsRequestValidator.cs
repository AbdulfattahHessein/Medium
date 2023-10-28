using FluentValidation;
using Medium.BL.Features.Reactions.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Reactions.Validators
{
    public class GetAllPaginationReactionsRequestValidator : AbstractValidator<GetAllPaginationReactionsRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPaginationReactionsRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.PageNumber).NotNull()
                .WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(p => p.PageSize).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName} Must be valid");

        }
    }
}
