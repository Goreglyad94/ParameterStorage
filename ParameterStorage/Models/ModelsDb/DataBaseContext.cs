using ParameterStorage.Data.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseContext
    {
        protected ParameterStorageDbContext context;
        public DataBaseContext()
        {
            context = new ParameterStorageDbContext();
        }
    }
}
