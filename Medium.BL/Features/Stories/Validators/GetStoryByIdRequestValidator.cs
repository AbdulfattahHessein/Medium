using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class GetStoryByIdRequestValidator : AbstractValidator<GetStoryByIdRequest>
    {
        public GetStoryByIdRequestValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .NotNull().WithMessage("Id Must be not null")
                .NotEmpty().WithMessage("Id Must be not empty");


        }
    }
}
