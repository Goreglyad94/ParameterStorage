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

        public virtual ProjectDto Project { get; set; }
        public virtual ICollection<FamilyDto> Families { get; set; }
        public override string ToString()
        {
            return ModelName;
        }
    }
}
