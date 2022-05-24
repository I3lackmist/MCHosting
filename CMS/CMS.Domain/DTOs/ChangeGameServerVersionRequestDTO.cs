namespace CMS.Domain.DTOs {
    public class ChangeGameServerVersionRequestDTO {
        public string requestingUserName { get; set; } = "";
        public string serverName { get; set; } = "";
        public string newVersion { get; set; } = "";
    }
}