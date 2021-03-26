using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class FamilyInstanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categoty { get; set; }
        public int FamilyInstanceId { get; set; }
        public int ModelId { get; set; }

        public virtual ModelDto ModelDto { get; set; }
        public virtual ICollection<ParameterInstanceDto> ParametersInstanceDto { get; set; }
    }
}
