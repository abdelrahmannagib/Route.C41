using Microsoft.Extensions.DependencyInjection;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Reopsitories;

namespace Route.C41.PL.Extensions
{
    public static class ApplicationServicesExensions
    {
       
            public static void AddApplicationServices(this IServiceCollection services)
            {
                services.AddScoped<IDepartmentRepository, DepartmentRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
      

            }
        
    }
}
