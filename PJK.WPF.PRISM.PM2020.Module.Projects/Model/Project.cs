using System;
using System.ComponentModel.DataAnnotations;


namespace PJK.WPF.PRISM.PM2020.Module.Projects.Model
{
    public class Project : IProject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SystemId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public int StatusID { get; set; }

        public bool Complete { get; set; }
        public string Comment { get; set; }
    }

}
