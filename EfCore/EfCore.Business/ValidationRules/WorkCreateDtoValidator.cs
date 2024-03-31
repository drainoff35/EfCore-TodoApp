using EfCore.Dtos.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Business.ValidationRules
{
    public class WorkCreateDtoValidator:AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x=>x.Definition).NotEmpty().WithMessage("İş tanımı boş bırakılamaz.");
        }
    }
}
