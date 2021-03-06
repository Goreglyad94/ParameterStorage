using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Data.DataBaseContext
{
    class ParameterStorageDbContext : DbContext
    {
        public ParameterStorageDbContext()
        {
            this.Database.Connection.ConnectionString = @"Data Source=nt-db01.ukkalita.local;Initial Catalog=M1_Revit;integrated security=True;MultipleActiveResultSets=True";
        }
        public DbSet<ProjectDto> Projects { get; set; }
        public DbSet<ModelDto> Models { get; set; }
        public DbSet<FamilyDto> Families { get; set; }
        public DbSet<ParameterInstanceDto> ParameterInstances { get; set; }
        public DbSet<ParameterTypeDto> ParameterTypes { get; set; }


        public DbSet<LogDto> LogsDto { get; set; }
    }
}
