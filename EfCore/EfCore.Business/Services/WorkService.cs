using AutoMapper;
using EfCore.Business.Interfaces;
using EfCore.Business.ValidationRules;
using EfCore.Common.ResponseObjects;
using EfCore.DataAccess.UnitOfWork;
using EfCore.Dtos.Interfaces;
using EfCore.Dtos.WorkDtos;
using EfCore.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;


        public WorkService(IUnitOfWork uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workCreateDto)
        {
            var validationResult = _createDtoValidator.Validate(workCreateDto);

            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(workCreateDto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, workCreateDto);
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (var error in validationResult.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
                return new Response<WorkCreateDto>(ResponseType.ValidationError, workCreateDto, errors);
            }

        }

        public async Task<IResponse> Delete(int id)
        {
            var deletedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Work>().Delete(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı. ");

        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {

            var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound,$"{id} ye ait data bulunamadı.");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto workUpdateDto)
        {
            var result = _updateDtoValidator.Validate(workUpdateDto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().GetById(workUpdateDto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(workUpdateDto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, workUpdateDto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, "data bulunamadı.");
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (var error in result.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, workUpdateDto, errors);
            }

        }
    }
}
