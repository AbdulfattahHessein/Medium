using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Publisher.Validators
{
    public class GetPublisherByIdRequestValidator : AbstractValidator<GetPublisherByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPublisherByIdRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.Id).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(p => p.Id).NotNull()
                .WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid")
                .MustAsync(async (gp, i, c) =>
                {
                    return await _unitOfWork.Publishers.AnyAsync(p => p.Id == gp.Id);
                })
                .WithMessage("Publisher is not found");
        }
    }
}
