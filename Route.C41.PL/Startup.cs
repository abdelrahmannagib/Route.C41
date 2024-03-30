using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Reopsitories;
using Route.C41.DAL.Data;
using Route.C41.PL.Extensions;
using Route.C41.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.PL
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; } //Conncetion String 

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddDbContext<ApplicationDbContext>(
				options =>
				//options.UseSqlServer("Server = localhost\\sqlexpress; Database = MVCS01; Trusted_Connection = True; TrustServerCertificate=true;")
				{
					options.UseSqlServer(Configuration.GetConnectionString("DeafultConnection"));

				}
				,ServiceLifetime.Scoped);
			services.AddApplicationServices();// Extension method
			services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
			//First Parm IS Dbcontextoptions
			// Deafult Scoped if we want to change pass parms
			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			///services.AddScoped<ApplicationDbContext>();
			///services.AddScoped<DbContextOptions<ApplicationDbContext>>();
			/// AddSingelton(One per all) AddTransient(Each request open connection)
		
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection(); // Http >> Https
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
