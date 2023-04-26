using AdbMe.CLI;

namespace AdbMe.Models
{
    public class DevicesViewModel
    {
        public Scrcpy Screanner { get; set; }
        private Task<Scrcpy> ScreenerTask { get; set; }
        public List<Device> ConnectedDevice { get; private set; }
        public bool IsScreenerLoaded { get; private set; } = false;
        public bool IsListReady { get; private set; }
        public string? ConnectTo { get; private set; }

        public DevicesViewModel() { }

        public DevicesViewModel(Scrcpy screanner, string? serial = null)
        {
            this.Screanner = screanner;
            this.ConnectTo = serial;
        }
    }

}