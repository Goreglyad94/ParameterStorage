using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParameterStorage.Models.FileSystemModel
{
    class OpenFileSettings
    {
        public List<string> GetCategoryList()
        {
            List<string> CategortList = new List<string>();
            List<string> SortCategortList = new List<string>();
            StreamReader f = new StreamReader(@"C:\ProgramData\Autodesk\Revit\Addins\2020\PaStorageSettings.txt", Encoding.UTF8);
            while (!f.EndOfStream)
            {
                string s = f.ReadLine().Replace(" ", "");
                CategortList.Add(s);
            }
            f.Close();

            int sliceIndex = CategortList.IndexOf("/////////////////////////////////////////////////////////////////////////////////");
            for (int i = 0; i < sliceIndex; i++)
            {
                SortCategortList.Add(CategortList[i]);
            }
            return SortCategortList;
        }


        public List<string> GetParametters()
        {
            List<string> ParamsList = new List<string>();
            List<string> SortParamsList = new List<string>();
            StreamReader f = new StreamReader(@"C:\ProgramData\Autodesk\Revit\Addins\2020\PaStorageSettings.txt", Encoding.UTF8);
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                ParamsList.Add(s);
            }
            f.Close();

            int sliceIndex = ParamsList.IndexOf("/////////////////////////////////////////////////////////////////////////////////");
            for (int i = sliceIndex+1; i < ParamsList.Count; i++)
            {
                SortParamsList.Add(ParamsList[i]);
            }
            return SortParamsList;
        }
    }
}
