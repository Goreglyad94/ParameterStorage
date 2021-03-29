using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ParameterStorageDto
{
    class LogDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string LoadTime { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }

        public virtual ProjectDto ProjectDto { get; set; }
    }
}
