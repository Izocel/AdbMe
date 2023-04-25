using System;
using System.IO;
using AdbMe.Models;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace AdbMe.CLI
{
    public sealed class Adb
    {
        private string Version { get; set; }
        private string SdkVersion { get; set; }
        private string InstalledPaths { get; set; }
        private string ShortName { get; set; } = "adb";
        public List<Device> ConnectedDevices { get; set; }
        public bool IsReady { get; private set; }

        public Adb() { }

        public async Task<Adb> Init(string binPath)
        {
            var strings = await GetVersion();

            this.Version = strings[0];
            this.SdkVersion = strings[1];
            this.InstalledPaths = binPath;
            this.ConnectedDevices = await GetDevices();

            this.IsReady = true;
            return this;
        }

        public async Task<string[]> GetVersion()
        {
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments("--version")
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            return stdOut.Split("\r\n");
        }

        public async Task<string[]> GetInstalledPaths()
        {
            var cmd = await Cli.Wrap("where")
                .WithArguments(ShortName)
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            return stdOut.Split("\r");
        }

        public async Task<List<Device>> GetDevices()
        {
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments(new[] { "devices", "-l" })
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;
            var strings = stdOut.Split("\r\n");
            
            List<Device> list = new List<Device>();
            foreach (var s in strings)
            {
                if(!s.Contains("transport_id")){
                    continue;
                } 
                var id = s.Split(" ")[0].Trim();
                var obj = new Device(id);
                list.Add(obj);
            }

            return list;
        }
    }
}