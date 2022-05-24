namespace CMS.Domain.DTOs {
    public class GameServerDTO {
        public string gameServerName { get; set; } = "";
        public string serverFlavorName { get; set; } = "";
        public string gameVersionName { get; set; } = "";
        public string ip { get; set; } = "";
        public string status { get; set; } = "";
    }
}