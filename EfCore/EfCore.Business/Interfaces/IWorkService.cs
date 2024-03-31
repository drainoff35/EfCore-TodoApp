using EfCore.Common.ResponseObjects;
using EfCore.Dtos.Interfaces;
using EfCore.Dtos.WorkDtos;
using EfCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Business.Interfaces
{
	public interface IWorkService
	{
		Task<IResponse<List<WorkListDto>>> GetAll();
		Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workCreateDto);
		Task<IResponse<IDto>> GetById<IDto>(int id);
		Task<IResponse> Delete(int id);
		Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto workUpdateDto);
	}
}
