using FluentValidation;
using Medium.BL.Features.StoryPhotos.Requests;

namespace Medium.BL.Features.StoryPhotos.Validators
{
    public class CreateStoryPhotoValidator : AbstractValidator<CreateStoryPhotoRequest>
    {
        public CreateStoryPhotoValidator()
        {
            RuleFor(p => p.Url).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName}Must be not empty");

        }
    }
}
