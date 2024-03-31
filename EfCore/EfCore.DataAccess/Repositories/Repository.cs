﻿using EfCore.DataAccess.Context;
using EfCore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class, new()
	{
		private readonly TodoContext _context;

		public Repository(TodoContext context)
		{
			_context = context;
		}

		public async Task Create(T entity)
		{
			 await _context.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public async Task<List<T>> GetAll()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
		{
			return asNoTracking ? await _context.Set<T>().SingleOrDefaultAsync(filter)
				: 
				await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
		}

		public async Task<T> GetById(object id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public IQueryable<T> GetQuery()
		{
			return	_context.Set<T>().AsQueryable();
		}

		public void Update(T entity,T unchanged)
		{
			_context.Entry(unchanged).CurrentValues.SetValues(entity);
		}
	}
}
