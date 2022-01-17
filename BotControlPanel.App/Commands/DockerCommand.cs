namespace BotControlPanel.App.Commands
{
    public static class DockerCommand
    {
        public static string Stats()
        {
            return "sudo docker container ls";
        }
        
        public static string Run(string imageName)
        {
            return $"sudo docker container start {imageName}";
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
            return "sudo docker stop $(docker ps -a -q)";
        }

        public static string Remove(string containerName)
        {
            return $"sudo docker rm {containerName}";
        }
    }
}