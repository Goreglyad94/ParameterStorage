using ParameterStorage.Models.ModelsDb;
using ParameterStorage.Models.ParameterStorageDto;
using ParameterStorage.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ParameterStorage.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        DataBaseUnload dataBaseUnload = new DataBaseUnload();
        List<ProjectDto> ProjectListDto { get; set; }
        public MainWindowViewModel()
        {
            ProjectListDto = dataBaseUnload.GetProjects();
            ProjectList = CollectionViewSource.GetDefaultView(ProjectListDto);
            ProjectList.Refresh();
            Title = "GGGGG";
        }

        #region Заголовок окна
        private string _Title;

        public string Title
        {
            get => _Title;
            set
            {
                //if (Equals(_Title, value)) return;
                //_Title = value;
                //OnPropertyChanged();
                Set(ref _Title, value);
            }
        }
        #endregion
        
        private ICollectionView projectList;
        public ICollectionView ProjectList
        {
            get => projectList;
            set => Set(ref projectList, value);
        }
    }
}
