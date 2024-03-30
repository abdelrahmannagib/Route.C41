using Microsoft.Extensions.DependencyInjection;
using Route.C41.BLL;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Reopsitories;

namespace Route.C41.PL.Extensions
{
    public static class ApplicationServicesExensions
    {
       
            public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOFWork, UnitOfWork>();
            return services;
            }
        
    }
}
