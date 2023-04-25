using LiteDB;

namespace AdbMe.Models
{
    public class Device : BaseModel
    {
        public string Primary { get; set; }
        public string Serial { get; set; }
        public string? LastIp { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Device() { }

        public Device(string serial, string? lastIp = null)
        {
            this.Primary = serial;
            this.Serial = serial;
            this.LastIp = lastIp;
        }

        public void InitBsonMapper()
        {
            BsonMapper.Global.Entity<Device>()
                .Id(x => x.Primary, false);
        }
    }
}