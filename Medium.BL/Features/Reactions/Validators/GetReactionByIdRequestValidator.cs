using FluentValidation;
using Medium.BL.Features.Reactions.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Reactions.Validators
{
    public class GetReactionByIdRequestValidator : AbstractValidator<GetReactionByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReactionByIdRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            RuleFor(p => p.Id).NotNull()
                .WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid")
                .MustAsync(async (dr, i, c) =>
                {
                    return await _unitOfWork.Reactions.AnyAsync(r => r.Id == dr.Id);
                })
                .WithMessage("Rection is not found");
        }
    }
}
