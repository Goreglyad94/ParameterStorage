using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParameterStorage.Infrastructure.Extentions;
using ParameterStorage.Models.ModelsDb;
using ParameterStorage.Models.ParameterStorageDto;
using ParameterStorage.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParameterStorage.RvtExternalEvent
{
    class ExEventGetFamiliesAndParameters : IExternalEventHandler
    {
        public static List<string> CategoryList { get; set; } = new List<string>();
        public static List<string> ParamsList { get; set; } = new List<string>();
        public static List<ModelDto> ModelList { get; set; } = new List<ModelDto>();

        List<BuiltInCategory> builtInCategories = new List<BuiltInCategory>();
        DataBaseFamilies dataBaseFamilies = new DataBaseFamilies();
        DataBaseParameters dataBaseParameters = new DataBaseParameters();
        DataBaseLogs GetLogs = new DataBaseLogs();
        public static ProjectDto ProjectDto;
        public void Execute(UIApplication app)
        {
            GetLogs.SetNewLog(null, "--", ProjectDto.Id);
            var stp_watch = new Stopwatch();
            stp_watch.Start();

            Document doc = app.ActiveUIDocument.Document;

            foreach (ModelDto model in ModelList)
            {
                using (Transaction loadDoc = new Transaction(doc, "Load Link Documents"))
                {
                    loadDoc.Start();
                    RevitLinkOptions rvtLinkOptions = new RevitLinkOptions(false);


                    ModelPath modelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(model.ModelPath);
                    LinkLoadResult linkType = RevitLinkType.Create(doc, modelPath, rvtLinkOptions);
                    RevitLinkInstance openedDocument = RevitLinkInstance.Create(doc, linkType.ElementId);


                    Document docLink = openedDocument.GetLinkDocument();
                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Добавление модели: " + model.ModelName, ProjectDto.Id);
                    stp_watch.Restart();
                    stp_watch.Start();
                    /* Получить все экземплярый и записать их в бд */
                    List<Element> instances = FindElements(docLink, GetCategoryFilter(), true).Where(x => x != null).ToList();
                    dataBaseFamilies.AddFamilies(instances.Select(x => new FamilyDto()
                    {
                        Categoty = x.Category.Name,
                        FamilyInstId = x.Id.IntegerValue,
                        FamilyTypeId = x.GetTypeId().IntegerValue,
                        ModelId = model.Id
                    }).ToList());

                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Создание нового списка семейств и запись в БД. Количество семейств: " + instances.Count, ProjectDto.Id);

                    stp_watch.Restart();
                    stp_watch.Start();
                    var FamlistFroDb = dataBaseFamilies.GetFamilyInstances(model);
                    List<ParameterInstanceDto> ParamsInstList = new List<ParameterInstanceDto>();
                    List<ParameterTypeDto> ParamsTypeList = new List<ParameterTypeDto>();
                    foreach (Element elem in instances)
                    {
                        var famId = FamlistFroDb.Where(x => x.FamilyInstId == elem.Id.IntegerValue).FirstOrDefault().Id;

                        ParamsInstList.AddRange(elem.Parameters.ToList().Select(x => new ParameterInstanceDto()
                        {
                            Name = x.Definition.Name,
                            FamilyId = famId
                        }).ToList());

                    }
                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Создание списка параметров экземпляров. Количество параметров: " + ParamsInstList.Count, ProjectDto.Id);
                    stp_watch.Restart();
                    stp_watch.Start();

                    foreach (Element elem in instances)
                    {
                        var famId = FamlistFroDb.Where(x => x.FamilyInstId == elem.Id.IntegerValue).FirstOrDefault().Id;

                        var elemType = docLink.GetElement(elem.GetTypeId());

                        if (elemType != null)
                            ParamsTypeList.AddRange((elemType.Parameters.ToList().Select(x => new ParameterTypeDto()
                            {
                                Name = x.Definition.Name,
                                FamilyId = famId
                            }).ToList()));
                    }

                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Создание списка параметров типов. Количество параметров: " + ParamsTypeList.Count, ProjectDto.Id);
                    stp_watch.Restart();
                    stp_watch.Start();

                    dataBaseParameters.AddParameterInstances(ParamsInstList);

                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Выгрузка параметров экземпляров в БД", ProjectDto.Id);
                    stp_watch.Restart();
                    stp_watch.Start();

                    dataBaseParameters.AddParameterTypes(ParamsTypeList);
                    stp_watch.Stop();
                    GetLogs.SetNewLog(stp_watch.Elapsed.ToString(), "Выгрузка параметров типов в БД", ProjectDto.Id);


                    loadDoc.RollBack();
                }
            }
            stp_watch.Stop();

            //GetLogs.SetNewLog(stp_watch.Elapsed.TotalMinutes.ToString(), "Выгрузка параметров", ProjectDto.Id);
            //TaskDialog.Show("Время выгрузки", upload_link_model.ToString());
        }

        public string GetName() => nameof(ExEventGetFamiliesAndParameters);

        private ElementMulticategoryFilter GetCategoryFilter()
        {
            foreach (var itemBuilt in Enum.GetValues(typeof(BuiltInCategory)))
                foreach (var itemString in CategoryList)
                {
                    string s = itemBuilt.ToString();
                    if (s == itemString)
                        builtInCategories.Add((BuiltInCategory)itemBuilt);
                }
            var multiCat = new ElementMulticategoryFilter(builtInCategories);
            return multiCat;
        }

        private IEnumerable<Element> FindElements(Document document, ElementMulticategoryFilter elmFilter, bool instances)
        {
            var collector = new FilteredElementCollector(document);
            return collector.WherePasses(CreateElementsFilter(instances, elmFilter));
        }
        private ElementFilter CreateElementsFilter(bool isInstancesFilter, ElementMulticategoryFilter elmFilter)
        {
            return new LogicalAndFilter(elmFilter, new ElementIsElementTypeFilter(isInstancesFilter));
        }

        private string GetParameterValue(Parameter parameter)
        {
            switch (parameter.StorageType)
            {
                case StorageType.Integer:
                    return parameter.AsInteger().ToString();

                case StorageType.Double:
                    return parameter.AsDouble().ToString();

                case StorageType.String:
                    return parameter.AsString().ToString();

                case StorageType.ElementId:
                    return parameter.AsValueString().ToString();

                case StorageType.None:
                    return null;

                default:
                    return null;
            }
        }
    }
}
