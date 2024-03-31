using AutoMapper;
using EfCore.Business.Interfaces;
using EfCore.Business.Mappings.AutoMapper;
using EfCore.Business.Services;
using EfCore.Business.ValidationRules;
using EfCore.DataAccess.Context;
using EfCore.DataAccess.UnitOfWork;
using EfCore.Dtos.WorkDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Business.DependencyResolvers.Microsoft
{
	public static class DependencyExtension
	{
		public static void AddDependencies(this IServiceCollection services)
		{
			
			services.AddDbContext<TodoContext>(
				opt =>
				{
					opt.UseSqlServer("server=(localdb)\\mssqllocaldb; database=ToDodb; integrated security=true;");
				});
			var configuration = new MapperConfiguration(opt =>
			{
				opt.AddProfile(new WorkProfile());
			});

			var mapper=configuration.CreateMapper();
			services.AddSingleton(mapper);
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IWorkService, WorkService>();
			services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }
	}
}
