using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Publisher.Validators
{
    public class UpdatePublisherRequestValidator : AbstractValidator<UpdatePublisherRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePublisherRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.Name).NotNull()
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");
        }

    }
}
