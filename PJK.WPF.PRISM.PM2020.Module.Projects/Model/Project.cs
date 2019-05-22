using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using System;
using System.ComponentModel.DataAnnotations;


namespace PJK.WPF.PRISM.PM2020.Module.Projects.Model
{
    public class Project : NotifyDataErrorInfoBase, IProject 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ProjectName { get; set; }

        
        public int SystemId { get; set; }

       
        public int Priority { get; set; }

       
        public DateTime Deadline { get; set; }

       
        public int StatusID { get; set; }

        public bool Complete { get; set; }
        public string Comment { get; set; }
    }

}
