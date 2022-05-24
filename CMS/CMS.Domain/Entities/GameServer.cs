using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities {
    [Table("GameServers")]
    public class GameServer : BaseEntity {
        [Required]
        public string GameServerName { get; set; } = "";
        [Required]
        public int ServerFlavorId { get; set; }
        [Required]
        public string GameVersionName { get; set; } = "";
        public string Ip { get; set; } = "";
    }
}