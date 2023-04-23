using System;
using System.IO;
using Acme.Models;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace Acme.CLI
{
    sealed class Srcpy
    {
        private Adb AdbCli = new Adb();
        private string Version { get; set; }
        private string Dependencies { get; set; }
        private string InstalledPaths { get; set; }
        private string ShortName { get; set; } = "scrcpy";
        private Device[] ConnectedDevices { get; set; }
        private Adb AbbCli { get; set; }
        public bool isReady { get; private set; }
        public bool IsRunning { get; private set; }
        public int? LastExitCode { get; private set; }
        public int? ProcessId { get; private set; }
        public string LastMessage { get; private set; }
        public string LastError { get; private set; }
        public CancellationTokenSource? Canceller { get; private set; }

        public Srcpy()
        {
            Init();
        }

        public async Task Init()
        {
            var strings = await GetVersion();
            this.Version = strings[0];
            strings[0] = "";
            this.Dependencies = String.Join("\n", strings).Trim();
            this.InstalledPaths = await GetInstalledPaths();

            this.AbbCli = new Adb();
            await AdbCli.Init(this.InstalledPaths);
            this.ConnectedDevices = AdbCli.ConnectedDevices;

            this.isReady = true;
        }

        public async Task Run(string serial)
        {
            try
            {
                while (!isReady);

                if (IsRunning)
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


