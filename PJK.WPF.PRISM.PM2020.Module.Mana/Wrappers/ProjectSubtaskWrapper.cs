using PJK.WPF.PRISM.PM2020.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Wrappers
{
    public class ProjectSubtaskWrapper : ModelWrapper<ProjectSubtask>
    {
        public ProjectSubtaskWrapper(ProjectSubtask model) : base(model)
        {

        }

        public string Subtask
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
