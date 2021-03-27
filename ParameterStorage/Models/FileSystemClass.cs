using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            List<string> modelPathes = new List<string>();
            var dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = false;
            var res = dlg.ShowDialog();
            if(res == DialogResult.OK)
            {
                
                StreamReader f = new StreamReader(dlg.FileName, Encoding.UTF8);
                while (!f.EndOfStream)
                {
                    string s = f.ReadLine();
                    modelPathes.Add(s);
                }
                f.Close();
            }
            return modelPathes;
        }
    }
}
