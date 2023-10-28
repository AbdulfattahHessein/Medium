﻿using FluentValidation;
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

            RuleFor(p => p.PublisherId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(p => p.FollowingId).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(p => p.FollowingId)
                .MustAsync(async (af, i, c) => await Task.Run(() => af.FollowingId != af.FollowingId))
                .WithMessage("Publisher can't follow itself");
        }
    }
}
