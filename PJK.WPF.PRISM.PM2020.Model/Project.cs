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
            ProjectSubtasks = new Collection<ProjectSubtask>();
        }


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        [StringLength(20)]
        [Display(Name = "Project Name 1")]
        public string ProjectName { get; set; }

        [Display(Name = "System ID")]
        [Required(ErrorMessage = "{0} is required")]
        public int? SystemId { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "{0} is required")]
        public int Priority { get; set; }

        [Display(Name = "Deadline")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Display(Name = "Complete")]
        public bool Complete { get; set; }
        [Display(Name = "Comments")]
        [Required(ErrorMessage = "{0} are required")]
        [StringLength(5)]
        public string Comment { get; set; }


        public int? SystemItemId { get; set; }
        public SystemItem SystemItem { get; set; }

        public ICollection<ProjectSubtask> ProjectSubtasks { get; set; }

    }
}
