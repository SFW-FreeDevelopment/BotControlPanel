using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotControlPanel.App.Commands;
using BotControlPanel.App.Models;
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
            RunCommand(command);
        }
        
        public void PullLatestImage(string imageName)
        {
            var command = DockerCommand.Pull(imageName);
            RunCommand(command);
        }
        
        public void KillContainer(string containerName)
        {
            var command = DockerCommand.Kill(containerName);
            RunCommand(command);
        }

        public void UpdateContainer(string containerName, string imageName)
        {
            var command = DockerCommand.Kill(containerName);
            RunCommand(command);

            command = DockerCommand.Remove(containerName);
            RunCommand(command);

            command = DockerCommand.Pull(imageName);
            RunCommand(command);
            
            command = DockerCommand.Run(imageName);
            RunCommand(command);
        }
        
        public void KillAllContainers()
        {
            var command = DockerCommand.KillAll();
            RunCommand(command);
        }

        public List<ContainerStatus> GetDockerStats()
        {
            var commandString = DockerCommand.Stats();
            
            using var client = new SshClient(_connectionInfo);
            client.Connect();
            var command = client.CreateCommand(commandString);
            var output = command.Execute();
            client.Disconnect();

            return output.Split("\n")
                .Skip(1)
                .SkipLast(1)
                .Select(str => str.Split("   ")
                    .Where(s => s != "").ToArray())
                .Select(row => new ContainerStatus
                {
                    ContainerId = row[0].Trim(),
                    Image = row[1].Trim(),
                    Command = row[2].Trim(),
                    Created = row[3].Trim(),
                    Status = row[4].Trim(),
                    Names = row[5].Trim()
                })
                .OrderBy(container => container.Image)
                .ToList();
        }
        
        private void RunCommand(string command)
        {
            using var client = new SshClient(_connectionInfo);
            client.Connect();
            client.RunCommand(command);
            client.Disconnect();
        }
    }
}