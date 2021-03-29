using ParameterStorage.Models.ParameterStorageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage.Models.ModelsDb
{
    class DataBaseLogs : DataBaseContext
    {
        public void SetNewLog(string loadTime, string dicription, int projectId)
        {
            LogDto newLog = new LogDto();
            newLog.Date = DateTime.Today.ToString();
            newLog.LoadTime = loadTime;
            newLog.Description = dicription;
            newLog.ComputerName = Environment.MachineName;
            newLog.UserName = Environment.UserName;

            newLog.ProjectId = projectId;

            context.LogsDto.Add(newLog);
            context.SaveChanges();
        }
    }
}
