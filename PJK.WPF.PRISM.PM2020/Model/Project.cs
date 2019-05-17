using System;
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020
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
