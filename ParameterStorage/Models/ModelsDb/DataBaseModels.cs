using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseModels : DataBaseContext
    {
        public List<ModelDto> GetModels(ProjectDto projectDto)
        {
            return context.Models.Where(x => x.ProjectId == projectDto.Id).ToList();
        }

        public void AddModels(ProjectDto projectDto)
        {
            FileSystemClass fileSystemClass = new FileSystemClass();
            List<string> modelPathes = fileSystemClass.GetModelsPath();

            List<ModelDto> Models = modelPathes.Select(x => new ModelDto() 
            { 
                ModelName = Path.GetFileName(x), 
                ModelPath = x, 
                ProjectId = projectDto.Id 
            }).ToList();

            context.Models.AddRange(Models);
            context.SaveChanges();
        }

        public void RemoveModels(ProjectDto projectDto)
        {
            try
            {
                foreach (var item in context.Models.Where(x => x.ProjectId == projectDto.Id))
                    context.Models.Remove(item);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
