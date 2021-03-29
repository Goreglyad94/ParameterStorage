using Autodesk.Revit.UI;
using ParameterStorage.Infrastructure.Commands;
using ParameterStorage.Models;
using ParameterStorage.Models.FileSystemModel;
using ParameterStorage.Models.ModelsDb;
using ParameterStorage.Models.ParameterStorageDto;
using ParameterStorage.RvtExternalEvent;
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
using System.Windows.Input;

namespace ParameterStorage.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        DataBaseProjects dataBaseProject = new DataBaseProjects();
        DataBaseModels dataBaseModels = new DataBaseModels();
        OpenFileSettings openFileSettings = new OpenFileSettings();
        OpenFileModelPathes getModelsPath = new OpenFileModelPathes();
        DataBaseLogs dataBaseLogs = new DataBaseLogs();
        public ExternalEvent externalEventUploadToDb;
        List<ProjectDto> ProjectListDto { get; set; }
        public MainWindowViewModel()
        {
            ProjectListDto = dataBaseProject.GetProjects();
            ProjectList = CollectionViewSource.GetDefaultView(ProjectListDto);
            ProjectList.Refresh();

            CategoryList = CollectionViewSource.GetDefaultView(openFileSettings.GetCategoryList());
            ProjectList.Refresh();

            ExEventGetFamiliesAndParameters.CategoryList = openFileSettings.GetCategoryList();


            ParameterList = CollectionViewSource.GetDefaultView(openFileSettings.GetParametters());
            ParameterList.Refresh();
            ExEventGetFamiliesAndParameters.ParamsList = openFileSettings.GetParametters();
            #region Комманды
            DeleteProjectCommand = new RelayCommand(OnDeleteProjectCommandExecutde, CanDeleteProjectCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddModelsCommand = new RelayCommand(OnAddModelsCommandExecutde, CanAddModelsCommandExecute);
            DeleteAllModelsCommand = new RelayCommand(OnDeleteAllModelsCommandExecutde, CanDeleteAllModelsCommandExecute);
            SelectSettingsFileCommand = new RelayCommand(OnSelectSettingsFileCommandExecutde, CanSelectSettingsFileCommandExecute);
            UploadToDBFileCommand = new RelayCommand(OnUploadToDBFileCommandExecutde, CanUploadToDBFileCommandExecute);
            #endregion
        }
        /*listBox список проектов~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Комманды
        #region Удаление проекта из ListBox
        public ICommand DeleteProjectCommand { get; set; }

        private void OnDeleteProjectCommandExecutde(object p)
        {
            dataBaseProject.RemoveProject(p as ProjectDto);
            ProjectListDto = dataBaseProject.GetProjects();
            ProjectList = CollectionViewSource.GetDefaultView(ProjectListDto);
            ProjectList.Refresh();
        }
        private bool CanDeleteProjectCommandExecute(object p) => true;
        #endregion

        #region Команда добавления нового проекта
        public ICommand AddNewProjectCommand { get; set; }

        private void OnAddNewProjectCommandExecutde(object p)
        {
            if (NewProjectName != null && NewProjectName != "")
                dataBaseProject.AddProject(new ProjectDto() { ProjectName = NewProjectName });
            else
                MessageBox.Show("Введите имя проекта", "Ошибка");

            ProjectListDto = dataBaseProject.GetProjects();
            ProjectList = CollectionViewSource.GetDefaultView(ProjectListDto);
            ProjectList.Refresh();
        }
        private bool CanAddNewProjectCommandExecute(object p) => true;
        #endregion
        #endregion

        #region Имя нового проекта проекта
        private string newProjectName;

        public string NewProjectName
        {
            get => newProjectName;
            set => Set(ref newProjectName, value);

        }
        #endregion

        #region КоллекшенВью для ListBox Projects
        private ICollectionView projectList;
        public ICollectionView ProjectList
        {
            get => projectList;
            set => Set(ref projectList, value);
        }
        #endregion

        #region SelectedItem листа проектов
        private ProjectDto selectedProject;

        public ProjectDto SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                List<ModelDto> ModelDtoList = dataBaseModels.GetModels(SelectedProject);
                ExEventGetFamiliesAndParameters.ModelList = ModelDtoList;
                ModelList = CollectionViewSource.GetDefaultView(ModelDtoList);
                ModelList.Refresh();
                
            }
        }

        #endregion

        /*ListBox список моделей~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Коммнады
        #region Добавление списка моделей. Выбор .txt файла
        public ICommand AddModelsCommand { get; set; }
        private void OnAddModelsCommandExecutde(object p)
        {
            dataBaseModels.AddModels(SelectedProject);
            ModelList = CollectionViewSource.GetDefaultView(dataBaseModels.GetModels(SelectedProject));
            ModelList.Refresh();
        }
        private bool CanAddModelsCommandExecute(object p)
        {
            if (SelectedProject != null)
                return true;
            else
                return false;
        }
        #endregion

        #region Удаление всех моделей из проекта
        public ICommand DeleteAllModelsCommand { get; set; }
        private void OnDeleteAllModelsCommandExecutde(object p)
        {
            dataBaseModels.RemoveModels(SelectedProject);
            ModelList = CollectionViewSource.GetDefaultView(dataBaseModels.GetModels(SelectedProject));
            ModelList.Refresh();
        }
        private bool CanDeleteAllModelsCommandExecute(object p) => true;
        #endregion


        #endregion

        #region КоллекшенВью для ListBox Models
        private ICollectionView modelList;
        public ICollectionView ModelList
        {
            get => modelList;
            set => Set(ref modelList, value);
        }
        #endregion

        /*Параметры выгрузки. Список параметров и категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region КоллекшенВью для ListBox Categoty
        private ICollectionView categoryList;
        public ICollectionView CategoryList
        {
            get => categoryList;
            set => Set(ref categoryList, value);
        }
        #endregion

        #region КоллекшенВью для ListBox Parameter
        private ICollectionView parameterList;
        public ICollectionView ParameterList
        {
            get => parameterList;
            set => Set(ref parameterList, value);
        }
        #endregion

        #region Комманды
        #region Выбрать файл настроек
        public ICommand SelectSettingsFileCommand { get; set; }
        private void OnSelectSettingsFileCommandExecutde(object p)
        {

        }
        private bool CanSelectSettingsFileCommandExecute(object p) => true;
        #endregion


        public ICommand UploadToDBFileCommand { get; set; }
        private void OnUploadToDBFileCommandExecutde(object p)
        {
            externalEventUploadToDb.Raise();
        }
        private bool CanUploadToDBFileCommandExecute(object p) => true;
        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    }
}
