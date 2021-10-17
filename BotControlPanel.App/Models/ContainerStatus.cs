namespace BotControlPanel.App.Models
{
    public class ContainerStatus
    {
        public string ContainerId { get; set; }
        public string Image { get; set; }
        public string Command { get; set; }
        public string Created { get; set; }
        public string Status { get; set; }
        public string Ports { get; set; }
        public string Names { get; set; }
    }
}