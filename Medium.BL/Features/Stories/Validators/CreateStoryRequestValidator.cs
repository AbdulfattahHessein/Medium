﻿using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class CreateStoryRequestValidator : AbstractValidator<CreateStoryRequest>
    {
        public CreateStoryRequestValidator()
        {
            RuleFor(s => s.Title)
                .NotNull().WithMessage("{PropertyName}Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not Empty");

            RuleFor(s => s.Content)
    .NotNull().WithMessage("{PropertyName}Must be not Null")
    .NotEmpty().WithMessage("{PropertyName}Must be not Empty");

            RuleFor(s => s.PublisherId)
    .NotNull().WithMessage("PublisherId Must be not null")
    .NotEmpty().WithMessage("PublisherId Must be not Empty");


        }
    }
}
