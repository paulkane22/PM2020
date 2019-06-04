using System.ComponentModel.DataAnnotations;

namespace PJK.WPF.PRISM.PM2020.Model
{
    public class SystemItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="System Name")]
        [StringLength(50)]
        public string SystemName { get; set; }

    }
}
