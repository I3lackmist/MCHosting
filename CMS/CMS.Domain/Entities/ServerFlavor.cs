using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities {
    [Table("ServerFlavors")]
    public class ServerFlavor : BaseEntity {
        [Required]
        public string ServerFlavorName { get; set; } = "";
        [Required]
        public string DisplayName { get; set; } = "";
        [Required]
        public int CPUs { get; set; }
        [Required]
        public int RAMGBs { get; set; }
        [Required]
        public int MemoryGBs { get; set; }
    }
}