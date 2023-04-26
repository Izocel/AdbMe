using System;
using System.IO;
using AdbMe.Models;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace AdbMe.CLI
{
    public sealed class Scrcpy
    {
        private string Version { get; set; }
        private string Dependencies { get; set; }
        private string InstalledPaths { get; set; }
        private string ShortName { get; set; } = "scrcpy";
        public List<Device> ConnectedDevices { get; set; }
        public Adb AdbCli { get; set; }
        public Nmap NmapCli { get; set; }
        public bool IsReady { get; private set; }
        public bool IsRunning { get; private set; }
        public int? LastExitCode { get; private set; }
        public int? ProcessId { get; private set; }
        public string LastMessage { get; private set; }
        public string LastError { get; private set; }
        public CancellationTokenSource? Canceller { get; private set; }

        public Scrcpy() { }

        public async Task<Scrcpy> Init()
        {
            var strings = await GetVersion();
            this.Version = strings[0];
            strings[0] = "";
            this.Dependencies = String.Join("\n", strings).Trim();
            this.InstalledPaths = await GetInstalledPaths();

            this.AdbCli = await new Adb().Init(this.InstalledPaths);
            this.NmapCli = await new Nmap().Init(this.InstalledPaths);
            this.ConnectedDevices = this.AdbCli.ConnectedDevices;

            this.IsReady = true;
            return this;
        }

        public async Task Run(string? serial)
        {
            try
            {
                while (!IsReady) ;

                if (IsRunning || serial?.Length < 1)
                {
                    return;
                }

                this.Canceller = new CancellationTokenSource();
                var cmdListener = Cli.Wrap(ShortName)
                    .WithArguments(new[] { "-s", serial })
                    .WithWorkingDirectory(Environment.CurrentDirectory)
                    .ListenAsync(Canceller.Token);

                this.IsRunning = true;
                await foreach (var cmdEvent in cmdListener)
                {
                    switch (cmdEvent)
                    {
                        case StartedCommandEvent started:
                            System.Console.WriteLine($"Process started; ID: {started.ProcessId}");
                            this.ProcessId = started.ProcessId;
                            break;
                        case StandardOutputCommandEvent stdOut:
                            System.Console.WriteLine($"Out> {stdOut.Text}");
                            this.LastMessage = stdOut.Text;
                            break;
                        case StandardErrorCommandEvent stdErr:
                            System.Console.WriteLine($"Err> {stdErr.Text}");
                            this.LastError = stdErr.Text;
                            break;
                        case ExitedCommandEvent exited:
                            System.Console.WriteLine($"Process exited; Code: {exited.ExitCode}");
                            this.LastExitCode = exited.ExitCode;
                            Dispose();
                            break;
                        default:
                            System.Console.WriteLine($"Something else has happened");
                            break;
                    }
                }

            }
            catch (OperationCanceledException)
            {
                Dispose();
            }
        }

        // Exit
        public void Cancel()
        {
            Canceller?.Cancel();
        }

        // Free up ressources
        public void Dispose()
        {
            Canceller?.Dispose();
            this.Canceller = null;
            this.ProcessId = null;
            this.IsRunning = false;
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

            return stdOut.Split("\r");
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
    }
}


