using EfCore.DataAccess.Configurations;
using EfCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DataAccess.Context
{
	public class TodoContext:DbContext
	{
        public TodoContext(DbContextOptions<TodoContext> options):base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new WorkConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Work> Works { get; set; }
	}
}
