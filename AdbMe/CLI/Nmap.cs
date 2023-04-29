using System;
using System.IO;
using AdbMe.Models;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace AdbMe.CLI
{
    public sealed class Nmap
    {
        private string Version { get; set; }
        private string SdkVersion { get; set; }
        private string InstalledPaths { get; set; }
        private string ShortName { get; set; } = "nmap";
        public List<Device> PotentialDevices { get; set; }
        public bool IsReady { get; private set; }
        public string ScannerIp { get; private set; }
        public string DefaultScannerIp { get; private set; }

        public Nmap() { 
            this.ScannerIp = "192.168.0.1/24";
            this.DefaultScannerIp = this.ScannerIp;
        }

        public async Task<Nmap> Init(string? binPath = null)
        {
            this.InstalledPaths = binPath ?? await GetInstalledPaths();
            this.Version = await GetVersion();
            this.PotentialDevices = await GetDevices();

            this.IsReady = true;
            return this;
        }

        public async Task<string> GetVersion()
        {
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments("--version")
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            return stdOut;
        }

        public async Task<string> GetInstalledPaths()
        {
            var cmd = await Cli.Wrap("where")
                .WithArguments(ShortName)
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            return stdOut.Split("\r")[0];
        }

        public async Task<List<Device>> GetDevices()
        {
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments(new[] { "-sn", ScannerIp })
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;
            var strings = stdOut.Split("\r\nNmap");

            List<Device> list = new List<Device>();
            foreach (var s in strings)
            {
                if (!s.Contains("scan report for")) {continue;}
                
                var ip = s.Split("\r")[0].Split("for")[1];

                if(ip.Contains('(')) {
                    ip = ip.Split(')')[0].Split('(')[1];
                }

                ip = ip.Trim();
                if (!Helpers.Helpers.IsIpValid(ip)) {continue;}

                var obj = new Device(ip, ip);
                list.Add(obj);
            }

            return list;
        }

    }
}