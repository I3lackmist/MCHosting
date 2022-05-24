namespace CMS.Domain.DTOs {
    public class CreateGameServerRequestDTO {
        public string gameServerName { get; set; } = "";
        public string gameVersionName { get; set; } = "";
        public string requestingUserName { get; set; } = "";
        public string serverFlavorName { get; set; } = "";
    }
}