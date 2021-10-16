using BotControlPanel.App.Commands;
using Renci.SshNet;

namespace BotControlPanel.App.Services
{
    public class BotService
    {
        private readonly ConnectionInfo _connectionInfo;
        
        public BotService(ConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }
        
        public void RunImage(string imageName)
        {
            var command = DockerCommand.Run(imageName);

            using var client = new SshClient(_connectionInfo);
            client.Connect();
            client.RunCommand(command);
        }
        
        public void PullLatestImage(string imageName)
        {
            var command = DockerCommand.Pull(imageName);

            using var client = new SshClient(_connectionInfo);
            client.Connect();
            client.RunCommand(command);
        }
        
        public void KillContainer(string containerName)
        {
            var command = DockerCommand.Kill(containerName);

            using var client = new SshClient(_connectionInfo);
            client.Connect();
            client.RunCommand(command);
        }
    }
}