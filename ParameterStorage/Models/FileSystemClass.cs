using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParameterStorage.Models
{
    class FileSystemClass
    {
        /// <summary>
        /// ShowDialog. Получить список моделей из .txt файла
        /// </summary>
        /// <returns></returns>
        public List<string> GetModelsPath()
        {
            var dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.ShowDialog();
            
            List<string> modelPathes = new List<string>();
            StreamReader f = new StreamReader(dlg.FileName, Encoding.GetEncoding("windows-1251"));
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                modelPathes.Add(s);
            }
            f.Close();

            return modelPathes;
        }
    }
}
