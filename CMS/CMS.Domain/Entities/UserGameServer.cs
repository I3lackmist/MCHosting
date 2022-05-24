using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities {
    [Table("User_GameServers")]
    public class UserGameServer : BaseEntity {
        [Required]
        [ForeignKey("User")]
        public int OwnerId { get; set; }
        [Required]
        [ForeignKey("GameServer")]
        public int GameServerId{ get; set; }
        [Required]
        public int OwnerRelativeId { get; set; }
    }
}