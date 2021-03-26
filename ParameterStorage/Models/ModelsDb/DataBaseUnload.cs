using ParameterStorage.Data.DataBaseContext;
using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public List<ProjectDto> RemoveProject(ProjectDto proj)
        {
            context.Projects.Remove(proj);
            context.SaveChanges();

            if (context.Projects != null)
                return context.Projects.ToList();
            else return null;
        }

        public List<ProjectDto> AddProject(ProjectDto proj)
        {
            if (context.Projects.Where(x => x.ProjectName == proj.ProjectName).Count() == 0)
            {
                context.Projects.Add(proj);
                context.SaveChanges();

                if (context.Projects != null)
                    return context.Projects.ToList();
                else return null;
            }

            else
            {
                MessageBox.Show("Такой проект уже есть", "Статус");
                return context.Projects.ToList();
            }
               
        }

    }
}
