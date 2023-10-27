using FluentValidation;
using Medium.BL.Features.Topics.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Topics.Validators
{
    public class DeleteTopicRequestValidator : AbstractValidator<DeleteTopicRequest>
    {
        public DeleteTopicRequestValidator() { 
            RuleFor(t=>t.Id).NotEmpty().WithMessage("{PropertyName} must be not empty")
                .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
