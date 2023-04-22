using Acme.Models;
using LiteDB;

namespace Migration.v100
{
    class CreateDeviceCollection
    {
        public static void Migrate(string[] args)
        {
            Console.WriteLine("Hello World!");
            LiteDatabase Db = new LiteDatabase(@"Acme/Database/Data/Acme.db");

            // Get a collection (or create, if doesn't exist)
            ILiteCollection<Device> col = Db.GetCollection<Device>("customers");

            // Index document using document Name property
            col.EnsureIndex(x => x.Name);

            // Query the collection
            List<Device> results = col.Query()
                .OrderBy(x => x.Name)
                .Limit(1)
                .ToList();

            if (results.Count > 0)
            {
                System.Console.WriteLine("Devices collections EOF");
                return;
            }

            // Base Seed
            System.Console.WriteLine("Device base seed was empty. Populating...");
            Device device = new Device
            {
                Name = "Device 1",
                LastIp = "192.168.0.228"
            };

            // Insert new device document (Id will be auto-incremented)
            col.Insert(device);
        }
    }
}