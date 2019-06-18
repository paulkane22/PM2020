using System.ComponentModel.DataAnnotations;

namespace PJK.WPF.PRISM.PM2020.Model
{
    public class ComboLookup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ComboName { get; set; }

        [Required]
        public int ComboGroupId { get; set; }

        [Required]
        public int ComboOrder { get; set; }

        [Required]
        public int ComboId { get; set; }


    }
}
