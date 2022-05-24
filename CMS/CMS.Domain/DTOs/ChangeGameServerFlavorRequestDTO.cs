namespace CMS.Domain.DTOs {
    public class ChangeGameServerFlavorRequestDTO {
        public string requestingUserName { get; set; } = "";
        public string serverName { get; set; } = "";
        public string newFlavor { get; set; } = "";
    }
}