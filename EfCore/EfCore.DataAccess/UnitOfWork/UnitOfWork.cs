using EfCore.DataAccess.Context;
using EfCore.DataAccess.Interfaces;
using EfCore.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TodoContext _todoContext;

		public UnitOfWork(TodoContext todoContext)
		{
			_todoContext = todoContext;
		}

		public IRepository<T> GetRepository<T>() where T : class, new()
		{
			return new Repository<T>(_todoContext);
		}

		public async Task SaveChanges()
		{
			await _todoContext.SaveChangesAsync();
		}
	}
}
