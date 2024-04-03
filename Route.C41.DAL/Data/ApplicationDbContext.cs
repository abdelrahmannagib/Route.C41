using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Data
{
	public class ApplicationDbContext: IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
		{

		}
		///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		///{
		///          //optionsBuilder.UseSqlServer();
	    ///         // optionsBuilder.UseSqlServer("Server = localhost\\sqlexpress; Database = ITI_DB; Trusted_Connection = True; TrustServerCertificate=true;");
		///
		///      }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee>	 Employees {  get; set; } 
	}
}
