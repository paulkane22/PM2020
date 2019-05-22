using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper
{
    public class ProjectWrapper : ModelWrapper<Project>
    {
        public ProjectWrapper(Project model) : base(model)
        {
        }

        public int Id {
            get { return Model.Id; }
            set { SetValue(value); }
        }


        public string ProjectName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int SystemId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int Priority
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public DateTime Deadline
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public int StatusID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public bool Complete
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(ProjectName):
                    if (string.Equals(ProjectName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are not valid projects";
                    }
                    break;
            }
        }
    }
}
