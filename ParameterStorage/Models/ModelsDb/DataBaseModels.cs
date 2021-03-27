using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseModels : DataBaseContext
    {
        public void GetModels(ProjectDto projectDto)
        {
            FileSystemClass fileSystemClass = new FileSystemClass();
            List<string> modelPathes = fileSystemClass.GetModelsPath();

            List<ModelDto> Models = modelPathes.Select(x => new ModelDto() 
            { 
                ModelName = Path.GetFileName(x), 
                ModelPath = x, 
                ProjectId = projectDto.Id 
            }).ToList();


        }

    }
}
