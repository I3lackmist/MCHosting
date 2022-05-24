namespace CMS.Domain.DTOs {
    public class SimpleResponse {
        
        public SimpleResponse() {}
        public SimpleResponse(string msg) {
            message = msg;    
        }

        public string message { get; set; } = "";
    }
}