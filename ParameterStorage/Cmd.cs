using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParameterStorage.Data.DataBaseContext;
using ParameterStorage.Models.ParameterStorageDto;
using ParameterStorage.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorage
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Cmd : IExternalCommand
    {
        const string serverName = @"Data Source=WS-176\SQLBIMDBENT;Initial Catalog=RvtMetadata;integrated security=True;MultipleActiveResultSets=True";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ParameterStorageDbContext contextPaStorage = new ParameterStorageDbContext();
            contextPaStorage.Database.Connection.ConnectionString = serverName;

            ProjectDto PAproject = new ProjectDto() { ProjectName = "1044 Petra Alexeeva" };
            contextPaStorage.Projects.Add(PAproject);

            contextPaStorage.SaveChanges();


            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();

            return Result.Succeeded;
        }
    }
}
