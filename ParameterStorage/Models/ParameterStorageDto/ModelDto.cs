using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class ModelDto
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelPath { get; set; }
        public int ProjectId { get; set; }

        public virtual ProjectDto ProjectDto { get; set; }
        public virtual ICollection<FamilyInstanceDto> FamiliesInstanceDto { get; set; }
        public virtual ICollection<FamilyTypeDto> FamiliesTypeDto { get; set; }
    }
}
