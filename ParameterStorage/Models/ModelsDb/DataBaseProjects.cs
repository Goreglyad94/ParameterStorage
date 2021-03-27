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
    class DataBaseProjects : DataBaseContext
    {
        /// <summary> Получить список проектов из БД </summary>
        public List<ProjectDto> GetProjects()
        {
            if (context.Projects != null)
                return context.Projects.ToList();

            else return null;
        }
        /// <summary> Удалить проект из БД </summary>
        public void RemoveProject(ProjectDto proj)
        {
            context.Projects.Remove(proj);
            context.SaveChanges();
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
