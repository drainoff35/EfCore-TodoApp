using EfCore.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Dtos.WorkDtos
{
    public class WorkUpdateDto:IDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Definition is required.")]
        public string Definition { get; set; }
        
        public bool IsCompleted { get; set; }
    }
}
