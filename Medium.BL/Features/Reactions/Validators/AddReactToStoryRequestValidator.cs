﻿using FluentValidation;
using Medium.BL.Features.Reactions.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Reactions.Validators
{
    public class AddReactToStoryRequestValidator : AbstractValidator<AddReactToStoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddReactToStoryRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.ReactionId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid")
                .MustAsync(async (ars, i, c) =>
                {
                    return await _unitOfWork.Reactions.AnyAsync(r => r.Id == ars.ReactionId);
                })
                .WithMessage("Rection is not found");

            //RuleFor(p => p.PublisherId).NotNull().
            //    WithMessage("{PropertyName} Must be not Null")
            //    .NotEmpty().WithMessage("{PropertyName} Must be valid")
            //    .MustAsync(async (ars, i, c) =>
            //    {
            //        return await _unitOfWork.Publishers.AnyAsync(p => p.Id == ars.PublisherId);
            //    })
            //    .WithMessage("Publisher is not found");


            RuleFor(p => p.StoryId).NotNull().
               WithMessage("{PropertyName} Must be not Null")
               .NotEmpty().WithMessage("{PropertyName} Must be valid")
               .MustAsync(async (ars, i, c) =>
               {
                   return await _unitOfWork.Stories.AnyAsync(s => s.Id == ars.StoryId);
               })
               .WithMessage("Story is not found");

        }
    }
}
