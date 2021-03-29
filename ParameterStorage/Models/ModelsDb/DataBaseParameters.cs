using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseParameters : DataBaseContext
    {
        public void AddParameterInstances(List<ParameterInstanceDto> parameterInstanceDto)
        {
            context.ParameterInstances.AddRange(parameterInstanceDto);
            context.SaveChanges();
        }
        public void AddParameterTypes(List<ParameterTypeDto> parameterTypeDto)
        {
            context.ParameterTypes.AddRange(parameterTypeDto);
            context.SaveChanges();
        }
    }
}
