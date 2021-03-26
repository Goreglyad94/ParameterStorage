using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class ParameterTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int FamilyTypeId { get; set; }

        public virtual FamilyTypeDto FamilyTypeDto { get; set; }
    }
}
