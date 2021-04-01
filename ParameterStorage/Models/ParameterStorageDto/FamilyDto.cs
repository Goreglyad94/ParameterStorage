using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class FamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categoty { get; set; }
        public int FamilyInstId { get; set; }
        public int FamilyTypeId { get; set; }
        public int ModelId { get; set; }

        public virtual ModelDto Model { get; set; }
        public virtual ICollection<ParameterInstanceDto> ParametersInstances { get; set; }
        public virtual ICollection<ParameterTypeDto> ParametersTypes { get; set; }

    }
}
