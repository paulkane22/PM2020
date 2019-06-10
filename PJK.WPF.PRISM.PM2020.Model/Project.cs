using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PJK.WPF.PRISM.PM2020.Model
{
    public class Project : IProject
    {
        public Project()
        {
            SubTasks = new Collection<ProjectSubtask>();
        }


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        [StringLength(20)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "System ID")]
        public int? SystemId { get; set; }

        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Display(Name = "Complete")]
        public bool Complete { get; set; }
        [Display(Name = "Comments")]
        public string Comment { get; set; }


        public SystemItem SystemItem { get; set; }

        public ICollection<ProjectSubtask> SubTasks { get; set; }

    }
}
