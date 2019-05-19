using System;
using System.ComponentModel.DataAnnotations;


namespace PJK.WPF.PRISM.PM2020.Module.Projects.Model
{
    public class Project : IProject
    {
        [Required]
        public int Id { get; set; }
        public int SystemId { get; set; }
        public string ProjectName { get; set; }
        public int Priority { get; set; }
        public DateTime Deadline { get; set; }
        public int StatusID { get; set; }
        public bool Complete { get; set; }
        public string Comment { get; set; }
    }

}
