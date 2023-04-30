using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;

namespace AdbMe.CLI
{
    public sealed class PlugAndPlayPs : Pwrshl
    {
        public PlugAndPlayPs() : base("Get-PnPDevice")
        {
            GetBluetoothAsync();
        }

        public async Task<string> GetWithTypeAsync(string type = "*" , bool strict = true) {
            var classMod = strict ? "-eq" : "-like";
            type = strict ? type : $"*{type}*";

            string call = " | Where-Object -FilterScript { $_.Class " +
                $"{classMod} '{type}' -and $_.FriendlyName -like '*' " + "}";

            string[] args = GetArgsWithInject(call);
            var cmd = await Cli.Wrap(ShortName)
                .WithArguments(args, false)
                .ExecuteBufferedAsync();

            var stdOut = cmd.StandardOutput;
            var stdErr = cmd.StandardError;
            var exitCode = cmd.ExitCode;

            var lines = stdOut.Split("\n");
            string[] headers = lines[1].Split("  ");
            string[] fields = headers.Where(x => x.Length > 0).ToArray();
            string[] items = lines.Where(x => x.Contains(type)).ToArray();

            foreach (var item in items)
            {
                // Parse key,value
            }

            return stdOut;
        }

        public async Task<string> GetUsbAsync()
        {
            return await GetWithTypeAsync("USB");
        }

        public async Task<string> GetBluetoothAsync()
        {
            return await GetWithTypeAsync("BLUETOOTH");
        }
    }
}