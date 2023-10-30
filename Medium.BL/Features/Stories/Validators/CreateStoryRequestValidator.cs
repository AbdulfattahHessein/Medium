using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class CreateStoryRequestValidator : AbstractValidator<CreateStoryRequest>
    {
        public CreateStoryRequestValidator()
        {
            RuleFor(s => s.Title)
                .NotNull().WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not Empty")
                .MinimumLength(3).WithMessage("{PropertyName} Must be greeter than or equal 3")
                .MaximumLength(30).WithMessage("{PropertyName} Must be less than or equal 30");


            RuleFor(s => s.Content)
    .NotNull().WithMessage("{PropertyName}Must be not Null")
    .NotEmpty().WithMessage("{PropertyName}Must be not Empty")
    .MinimumLength(5).WithMessage("{PropertyName} Must be greeter than or equal 5");

            //        RuleFor(s => s.PublisherId)
            //.NotNull().WithMessage("PublisherId Must be not null")
            //.NotEmpty().WithMessage("PublisherId Must be not Empty");


        }
    }
}
