namespace CMS.Domain.DTOs {
    public class ChangeGameServerNameRequestDTO {
        public string requestingUserName { get; set; } = "";
        public string serverName { get; set; } = "";
        public string newName { get; set; } = "";
    }
}