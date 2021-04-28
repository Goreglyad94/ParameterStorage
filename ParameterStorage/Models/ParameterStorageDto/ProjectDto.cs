using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<ModelDto> Models { get; set; }
        public virtual ICollection<LogDto> Logs { get; set; }
        public override string ToString()
        {
            return ProjectName; 
        }
    }
}
