using FluentValidation;
using Medium.BL.Features.Reactions.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Reactions.Validators
{
    public class UpdateReactionRequestValidator : AbstractValidator<UpdateReactionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReactionRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(r => r.Id).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid")
                .MustAsync(async (ur, i, c) =>
                {
                    return await _unitOfWork.Reactions.AnyAsync(r => r.Id == ur.Id);
                })
                .WithMessage("Publisher is not found");


            RuleFor(p => p.Name).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName} Must be not empty");


        }
    }
}
