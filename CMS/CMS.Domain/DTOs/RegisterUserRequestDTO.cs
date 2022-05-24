namespace CMS.Domain.DTOs {
    public class RegisterUserRequestDTO {
        public string userName { get; set; } = "";
        public string password { get; set; } = "";
        public string email { get; set; } = "";
    }
}