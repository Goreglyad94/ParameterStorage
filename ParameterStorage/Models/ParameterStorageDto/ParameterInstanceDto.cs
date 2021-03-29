using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class ParameterInstanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public int FamilyId { get; set; }

        public virtual FamilyDto FamilyDto { get; set; }
    }
}
