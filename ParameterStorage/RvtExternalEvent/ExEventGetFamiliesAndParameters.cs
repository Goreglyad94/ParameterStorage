using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParameterStorage.Infrastructure.Extentions;
using ParameterStorage.Models.ModelsDb;
using ParameterStorage.Models.ParameterStorageDto;
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
        public void Execute(UIApplication app)
        {

            Document doc = app.ActiveUIDocument.Document;

            foreach (ModelDto model in ModelList)
            {
                using (Transaction loadDoc = new Transaction(doc, "Load Link Documents"))
                {
                    var stp_watch = new Stopwatch();
                    //Открытие и загрузка файла
                    stp_watch.Start();
                    loadDoc.Start();
                    RevitLinkOptions rvtLinkOptions = new RevitLinkOptions(false);


                    ModelPath modelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(model.ModelPath);
                    LinkLoadResult linkType = RevitLinkType.Create(doc, modelPath, rvtLinkOptions);
                    RevitLinkInstance openedDocument = RevitLinkInstance.Create(doc, linkType.ElementId);


                    Document docLink = openedDocument.GetLinkDocument();

                    /* Получить все экземплярый и записать их в бд */
                    List<Element> instances = FindElements(docLink, GetCategoryFilter(), true).Where(x => x != null).ToList();
                    List<FamilyDto> FamDto = new List<FamilyDto>();

                    foreach (Element elem in instances)
                    {
                        FamilyDto familyDto = new FamilyDto();
                        familyDto.Categoty = elem.Category.Name;
                        familyDto.FamilyInstId = elem.Id.IntegerValue;
                        familyDto.FamilyTypeId = elem.GetTypeId().IntegerValue;
                        familyDto.ModelId = model.Id;
                        FamDto.Add(familyDto);
                    }
                    dataBaseFamilies.AddFamilies(FamDto);



                    #region Какой то код. Удалить если не вспомнишь, зачем он
                    //foreach (var item in instances)
                    //{
                    //    dataBaseFamilies.AddFamily(item);
                    //}
                    //stp_watch.Stop();
                    //MessageBox.Show(stp_watch.Elapsed.TotalSeconds.ToString() + " " + instances.Count());


                    //List<ParameterInstanceDto> ParametersInstanceDto = new List<ParameterInstanceDto>();
                    //var stp_watch = new Stopwatch();
                    ////Открытие и загрузка файла
                    //stp_watch.Start();

                    //var ddd = dataBaseFamilies.GetFamilyInstances(model);

                    //foreach (var item in ddd)
                    //{
                    //    Element family = docLink.GetElement(new ElementId(item.Id));
                    //    if (family != null)
                    //    {
                    //        foreach (var param in family.Parameters.ToList())
                    //        {
                    //            ParameterInstanceDto parameterInstanceDto = new ParameterInstanceDto();
                    //            parameterInstanceDto.Name = param.Definition.Name;
                    //            parameterInstanceDto.FamInstanceId = item.Id;
                    //            parameterInstanceDto.Value = GetParameterValue(param);
                    //            ParametersInstanceDto.Add(parameterInstanceDto);
                    //        }
                    //    }
                    //}
                    //stp_watch.Stop();
                    //var upload_link_model = stp_watch.Elapsed.TotalSeconds;
                    //MessageBox.Show(ParametersInstanceDto.Count.ToString() + " " + upload_link_model.ToString()); 
                    #endregion

                    var FamlistFroDb = dataBaseFamilies.GetFamilyInstances(model);
                    List<ParameterInstanceDto> ParamsInstList = new List<ParameterInstanceDto>();
                    List<ParameterTypeDto> ParamsTypeList = new List<ParameterTypeDto>();
                    foreach (Element elem in instances)
                    {
                        foreach (Parameter param in elem.Parameters.ToList())
                        {
                            ParameterInstanceDto parameterinstancedto = new ParameterInstanceDto();
                            parameterinstancedto.Name = param.Definition.Name;
                            parameterinstancedto.FamilyId = FamlistFroDb.Where(x => x.FamilyInstId == elem.Id.IntegerValue).FirstOrDefault().Id;
                            //parameterinstancedto.value = getparametervalue(param);
                            ParamsInstList.Add(parameterinstancedto);
                        }

                        var elemType = docLink.GetElement(elem.GetTypeId());
                        if (elemType != null)
                        {
                            foreach (Parameter param in elemType.Parameters.ToList())
                            {
                                ParameterTypeDto parameterTypeDto = new ParameterTypeDto();
                                parameterTypeDto.Name = param.Definition.Name;
                                parameterTypeDto.FamilyId = FamlistFroDb.Where(x => x.FamilyInstId == elem.Id.IntegerValue).FirstOrDefault().Id;
                                ParamsTypeList.Add(parameterTypeDto);
                            }
                        }
                    }
                    dataBaseParameters.AddParameterInstances(ParamsInstList);
                    dataBaseParameters.AddParameterTypes(ParamsTypeList);

                    loadDoc.RollBack();
                }
            }
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
