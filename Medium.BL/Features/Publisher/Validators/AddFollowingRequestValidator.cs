using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Publisher.Validators
{
    public class AddFollowingRequestValidator : AbstractValidator<AddFollowingRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFollowingRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.FollowingId)
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

        }
    }
}
