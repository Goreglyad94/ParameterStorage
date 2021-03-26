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
        public int FamInstanceId { get; set; }

        public virtual FamilyInstanceDto FamilyInstanceDto { get; set; }
    }
}
