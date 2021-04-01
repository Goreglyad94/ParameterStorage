using ParameterStorageConsole.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorageConsole
{
    class ParameterStorageDbContext : DbContext
    {
        public ParameterStorageDbContext()
        {
            this.Database.Connection.ConnectionString = @"Data Source=WS-176\SQLBIMDBENT;Initial Catalog=TestDbEf;integrated security=True;MultipleActiveResultSets=True";
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
