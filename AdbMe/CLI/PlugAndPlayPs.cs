using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace AdbMe.CLI
{
    public sealed class PlugAndPlayPs : Pwrshl
    {
        public PlugAndPlayPs() : base("Get-PnPDevice")
        {
            GetUsbAsync("Samsung");
        }

        public async Task<string> GetWithFiltersAsync(string type = "TYPE", string name = "*", bool strictName = false, bool strictType = false)
        {
            var classMod = strictType ? "-eq" : "-like";
            type = strictType ? type : $"*{type}*";

            var nameMod = strictName ? "-eq" : "-like";
            name = strictName ? name : $"*{name}*";

            string call = " -PresentOnly | Where-Object -FilterScript { $_.Class " +
                $"{classMod} '{type}' -and $_.FriendlyName {nameMod} '{name}' " + "}";

            string[] args = GetArgsWithInject(call);
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments(args, false)
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            var lines = stdOut.Split("\n");
            string[] headers = lines[1].Split("  ");
            string[] fields = headers.Where(x => x.Length > 0).ToArray();
            fields[fields.Count()-1] = fields[fields.Count()-1].Replace(" \r", "");
            
            string[] items = lines.Where(x => 
                !x.StartsWith("\r")
                && !x.StartsWith("Status")
                && !x.StartsWith("--")
                && x.Trim() != ""
            ).ToArray();

            foreach (var item in items)
            {
                // Parse key,value
            }

            return stdOut;
        }

        public async Task<string> GetUsbAsync(string name = "*")
        {
            return await GetWithFiltersAsync("USB", name);
        }

        public async Task<string> GetBluetoothAsync(string name = "*")
        {
            return await GetWithFiltersAsync("BLUETOOTH", name);
        }

        public async Task<string> GetModemAsync(string name = "*")
        {
            return await GetWithFiltersAsync("MODEM", name);
        }

        public async Task<string> GetWindowPortablesAsync(string name = "*")
        {
            return await GetWithFiltersAsync("WPD", name);
        }
    }
}