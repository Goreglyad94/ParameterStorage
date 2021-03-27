using ParameterStorage.Infrastructure.Commands;
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
using System.Windows.Input;

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
            #region Комманды
            DeleteProjectCommand = new RelayCommand(OnDeleteProjectCommandExecutde, CanDeleteProjectCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddModelsCommand = new RelayCommand(OnAddModelsCommandExecutde, CanAddModelsCommandExecute);
            #endregion
        }
        /*listBox список проектов~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Комманды
        #region Удаление проекта из ListBox
        public ICommand DeleteProjectCommand { get; set; }

        private void OnDeleteProjectCommandExecutde(object p)
        {
            dataBaseUnload.RemoveProject(p as ProjectDto);
            ProjectListDto = dataBaseUnload.GetProjects();
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
                dataBaseUnload.AddProject(new ProjectDto() { ProjectName = NewProjectName });
            else
                MessageBox.Show("Статус", "Введите имя проекта");

            ProjectListDto = dataBaseUnload.GetProjects();
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
            }
        }

        #endregion

        /*ListBox список моделей~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Коммнады
        #region Добавление списка моделей. Выбор .txt файла
        public ICommand AddModelsCommand { get; set; }
        private void OnAddModelsCommandExecutde(object p)
        {
            
        }
        private bool CanAddModelsCommandExecute(object p) => true;
        #endregion
        #endregion


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    }
}
