namespace BotControlPanel.App.Commands
{
    public static class DockerCommand
    {
        public static string Stats()
        {
            return "sudo docker stats";
        }
        
        public static string Run(string imageName)
        {
            return $"sudo docker container run {imageName}";
        }
        
        public static string Pull(string imageName)
        {
            return $"sudo docker pull {imageName}";
        }
        
        public static string Kill(string containerName)
        {
            return $"sudo docker kill {containerName}";
        }

        public static string KillAll()
        {
            return "docker stop $(docker ps -a -q)";
        }
    }
}