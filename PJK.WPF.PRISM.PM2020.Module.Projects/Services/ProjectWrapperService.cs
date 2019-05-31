using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public class ProjectWrapperService
    {

        private IProjectDataService _dataService;
        public ProjectWrapperService(IProjectDataService dataService)
        {
            _dataService = dataService;
            this.LoadProjectWrappers();
        }

        private ObservableCollection<ProjectWrapper> _projectWrappers = new ObservableCollection<ProjectWrapper>();
        public ObservableCollection<ProjectWrapper> ProjectWrappers
        {
            get { return _projectWrappers; }
            set { _projectWrappers = value; }
        }

        #region LoadWrappers
        private async void LoadProjectWrappers()
        {
            //sets up the list of Projects 
            
            int count = 1;

            //Task<int> task = new Task<int>(NewMethod);
            //task.Start();
            //count = await task; //NewMethod();
            NewMethod();
        }

        private int NewMethod()
        {
            int count = 1;
            ObservableCollection<Project> projects = _dataService.GetProjects();
            foreach (Project project in projects)
            {
                ProjectWrapper myProjectWrapper = new ProjectWrapper(project);
                myProjectWrapper.Id = count;
                this.ProjectWrappers.Add(myProjectWrapper);
                count++;

            }

            return count;
        }

        #endregion LoadWrappers

    }
}
