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

        public DevicesViewModel() { }

        public DevicesViewModel(Scrcpy screanner)
        {
            this.Screanner = screanner;
        }
    }

}