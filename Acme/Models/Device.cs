using LiteDB;

namespace Acme.Models
{
    public class Device : BaseModel
    {
        public string Primary { get; set; }
        public string Name { get; set; }
        public string? LastIp { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Device() { }

        public Device(string name, string? lastIp = null)
        {
            this.Primary = name;
            this.Name = name;
            this.LastIp = lastIp;
            this.InitBsonMapper();
        }

        public void InitBsonMapper()
        {
            BsonMapper.Global.Entity<Device>()
                .Id(x => x.Primary, false);
        }
    }
}