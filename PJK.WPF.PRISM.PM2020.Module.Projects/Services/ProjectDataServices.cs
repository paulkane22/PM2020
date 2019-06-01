﻿using PJK.WPF.PRISM.PM2020.DataAccess;
using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    class ProjectDataService :  IProjectDataService
    {
        private ProjectList projects;


        public async Task<List<Project>> GetAllAsync()
        {
            using (var ctx = new PM202DbContext())
            {
                return await ctx.Projects.AsNoTracking().ToListAsync();
            }

        }


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
            //BigLoad(1000);
            return this.projects;
        }

        public void BigLoad(int number)
        {
            for (int a = 10; a < number; a++)
            {
                Project k = new Project()
                {
                    Id = a,
                    SystemId = 1,
                    ProjectName = "HCIS " + a.ToString(),
                    Priority = 1,
                    StatusID = 1
                };

                projects.Add(k);
            }
        }




    }








}


