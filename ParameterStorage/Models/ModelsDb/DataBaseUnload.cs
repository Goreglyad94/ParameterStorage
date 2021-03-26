using ParameterStorage.Data.DataBaseContext;
using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseUnload
    {
        ParameterStorageDbContext context;
        public DataBaseUnload()
        {
            context = new ParameterStorageDbContext();
            context.Database.Connection.ConnectionString = @"Data Source=WS-176\SQLBIMDBENT;Initial Catalog=RvtMetadata;integrated security=True;MultipleActiveResultSets=True";
        }

        public List<ProjectDto> GetProjects()
        {
            if (context.Projects != null)
                return context.Projects.ToList();

            else return null;
        }
    }
}
