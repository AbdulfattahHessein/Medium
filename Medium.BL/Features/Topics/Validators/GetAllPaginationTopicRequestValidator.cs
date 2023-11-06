using FluentValidation;
using Medium.BL.Features.Topics.Request;

namespace Medium.BL.Features.Topics.Validators
{
    public class GetAllPaginationTopicRequestValidator : AbstractValidator<GetAllPaginationTopicRequest>
    {
        public GetAllPaginationTopicRequestValidator()
        {
            RuleFor(t => t.PageNumber).NotNull()
              .WithMessage("{PropertyName} Must be not Null")
              .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(t => t.PageSize).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }
}
