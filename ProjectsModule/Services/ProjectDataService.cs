using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsModule.Model;

namespace ProjectsModule.Services
{
    class ProjectDataService : IProjectDataService
    {
        private Projects projects;


        public Projects GetProjects()
        {
            if (this.projects == null)
            {
                this.projects = new Projects
                {
                    new Project()
                        {
                            Id = 1,
                            SystemId = 1,
                            ProjectName = "HCIS 2",
                            Priority = 1,
                            StatusID = 1
                        },
                    new Project()
                        {
                            Id = 2,
                            ProjectName = "ITP 2",
                            Priority = 1,
                            StatusID = 1
                        },
                    new Project()
                        {
                            Id = 3,
                            ProjectName = "WOUNDCARE",
                            Priority = 1,
                            StatusID = 1
                        },
                    new Project()
                        {
                            Id = 4,
                             ProjectName = "SPINAL REFERRAL",
                            Priority = 1,
                            StatusID = 1
                        },
                    new Project()
                        {
                            Id = 5,
                            ProjectName = "IVIG",
                            Priority = 1,
                            StatusID = 1
                        },
                    new Project()
                        {
                            Id = 6,
                               ProjectName = "NHD",
                            Priority = 1,
                            StatusID = 1
                        }
                };
            }
            return this.projects;
        }
    }
}
