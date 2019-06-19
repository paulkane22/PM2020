using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Model
{
    public class ProjectSubtask
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subtask { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

    }
}
