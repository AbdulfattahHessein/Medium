using FluentValidation;
using Medium.BL.Features.Topics.Request;

namespace Medium.BL.Features.Topics.Validators
{
    public class GetTopicByIdRequestValidator : AbstractValidator<GetTopicByIdRequest>
    {
        public GetTopicByIdRequestValidator()
        {
            RuleFor(t => t.Id).NotEmpty().WithMessage("{PropertyName} must be not empty")
               .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
