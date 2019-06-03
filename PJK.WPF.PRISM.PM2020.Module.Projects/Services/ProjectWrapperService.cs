using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public class ProjectWrapperService
    {

        private IProjectDataService _dataService;
        public ProjectWrapperService(IProjectDataService dataService)
        {
            _dataService = dataService;
            // this.LoadProjectWrappers();
        }


        //private ObservableCollection<ProjectWrapper> _projectWrappers = new ObservableCollection<ProjectWrapper>();
        //public ObservableCollection<ProjectWrapper> ProjectWrappers
        //{
        //    get { return _projectWrappers; }
        //    set { _projectWrappers = value; }
        //}

        //#region LoadWrappers
        //public async Task LoadProjectWrappersAsync()
        //{
        //  await  NewMethodAsync();
        //}

        //private async Task NewMethodAsync()
        //{
        //    int count = 1;
        //    System.Collections.Generic.IEnumerable<Project> projects = await _dataService.GetProjects(
        //    foreach (Project project in projects)
        //    {
        //        ProjectWrapper myProjectWrapper = new ProjectWrapper(project);
        //        myProjectWrapper.Id = count;
        //        this.ProjectWrappers.Add(myProjectWrapper);
        //        count++;

        //    }

        //   //
        //}

        //#endregion LoadWrappers

    }
}
