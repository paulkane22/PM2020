using PJK.WPF.PRISM.PM2020.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper
{
    public class SubTaskWrapper : ModelWrapper<ProjectSubtask>
    {
        public SubTaskWrapper(ProjectSubtask model) : base(model)
        {

        }

        private string _subtask;
        public string Subtask
        {
            get { return _subtask; }
            set { SetProperty(ref _subtask, value); }
        }

    }
}
