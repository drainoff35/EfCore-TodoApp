using EfCore.Dtos.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Business.ValidationRules
{
    public class WorkUpdateDtoValidator:AbstractValidator<WorkUpdateDto>
    {
        public WorkUpdateDtoValidator()
        {
            RuleFor(x=>x.Definition).NotEmpty().WithMessage("Güncellenecek iş tanımı boş bırakılamaz.");
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
