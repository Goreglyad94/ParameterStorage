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
            
            this.Database.Connection.ConnectionString = @"Data Source=WS-176\SQLBIMDBENT;Initial Catalog=RvtMetadata;integrated security=True;MultipleActiveResultSets=True";
        }
        public DbSet<ProjectDto> Projects { get; set; }
        public DbSet<ModelDto> Models { get; set; }
        public DbSet<FamilyInstanceDto> FamilyInstances { get; set; }
        public DbSet<FamilyTypeDto> FamilyTypes { get; set; }
        public DbSet<ParameterInstanceDto> ParameterInstances { get; set; }
        public DbSet<ParameterTypeDto> ParameterTypes { get; set; }
    }
}
