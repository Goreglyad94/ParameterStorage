using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Infrastructure.Extentions
{
    public static class ParamtersExtention
    {
        public static List<Parameter> ToList(this ParameterSet ps)
        {
            var parameters = new List<Parameter>();
            foreach (Parameter param in ps)
            {
                parameters.Add(param);
            }
            return parameters;
        }
    }
}
