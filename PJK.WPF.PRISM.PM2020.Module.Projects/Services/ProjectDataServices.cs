using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    class ProjectDataService :  IProjectDataService
    {
        private ProjectList projects;


        public ProjectList GetProjects()
        {
            if (this.projects == null)
            {
                this.projects = new ProjectList
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


