namespace CMS.Domain.DTOs {
    public class ServerFlavorDTO {
        public string displayName { get; set; } = "";
        public int cpus { get; set; }
        public int ramGBs { get; set; }
        public int memoryGBs { get; set; }
    }
}