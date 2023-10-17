using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class CreateStoryRequestValidator : AbstractValidator<CreateStoryRequest>
    {
        public CreateStoryRequestValidator()
        {

        }
    }
}
