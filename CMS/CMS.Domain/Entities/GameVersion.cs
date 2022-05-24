using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities {
    [Table("GameVersions")]
    public class GameVersion : BaseEntity {
        [Required]
        public string GameVersionName { get; set; } = "";
    }
}