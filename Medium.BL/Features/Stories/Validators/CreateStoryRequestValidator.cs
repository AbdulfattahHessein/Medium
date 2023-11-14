using FluentValidation;
using Medium.BL.Features.Stories.Requests;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Stories.Validators
{
    public class CreateStoryRequestValidator : AbstractValidator<CreateStoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStoryRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(s => s.Title)
                .NotEmpty().WithMessage("{PropertyName} Must be not Empty");

            RuleFor(s => s.Content)
                .NotEmpty().WithMessage("{PropertyName}Must be not Empty");

            RuleFor(s => s.Topics)
                .NotNull().WithMessage("{PropertyName} must not be null")
                .Must(topics => topics != null && topics.All(topic => !string.IsNullOrWhiteSpace(topic)))
                .WithMessage("Topic names must not be null or empty")
                .Must(topics => topics == null || topics.Distinct().Count == topics.Count)
                .WithMessage("Topic names must be unique");

            //        RuleFor(s => s.PublisherId)
            //.NotNull().WithMessage("PublisherId Must be not null")
            //.NotEmpty().WithMessage("PublisherId Must be not Empty");


        }
    }
}
