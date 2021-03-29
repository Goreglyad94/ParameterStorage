using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseFamilies : DataBaseContext
    {
        public FamilyDto AddFamily(FamilyDto family)
        {
            context.Families.Add(family);
            context.SaveChanges();
            return family;
        }

        public void AddFamilies(List<FamilyDto> families)
        {
            context.Families.AddRange(families);
            context.SaveChanges();
        }

        public List<FamilyDto> GetFamilyInstances(ModelDto modelDto)
        {
            return context.Families.Where(x => x.ModelId == modelDto.Id).ToList();

        }
    }
}
