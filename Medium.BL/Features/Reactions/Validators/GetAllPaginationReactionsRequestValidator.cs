using FluentValidation;
using Medium.BL.Features.Reactions.Request;

namespace Medium.BL.Features.Reactions.Validators
{
    public class GetAllPaginationReactionsRequestValidator : AbstractValidator<GetAllPaginationReactionsRequest>
    {
        public GetAllPaginationReactionsRequestValidator()
        {

            RuleFor(p => p.PageNumber).NotNull()
                .WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(p => p.PageSize).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName} Must be valid");

        }
    }
}
