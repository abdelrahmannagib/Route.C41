using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Data.Configurations
{
	internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(nameof(Department.ID)).UseIdentityColumn(10,10);
			builder.Property(nameof(Department.Code)).HasColumnType("varchar").HasMaxLength(50);
			builder.Property(nameof(Department.Name)).HasColumnType("varchar").HasMaxLength(50);


		}
	}
}
