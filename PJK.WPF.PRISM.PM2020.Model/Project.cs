using System;
using System.ComponentModel.DataAnnotations;

namespace PJK.WPF.PRISM.PM2020.Model
{
    public class Project : IProject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} is required bro")]
        [StringLength(20)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "ID")]
        public int SystemId { get; set; }

        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; }
        [Display(Name = "Complete")]
        public bool Complete { get; set; }
        [Display(Name = "Comments")]
        public string Comment { get; set; }

    }
}
