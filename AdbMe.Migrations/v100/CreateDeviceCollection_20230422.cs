using AdbMe.Models;
using LiteDB;

namespace Migration.v100
{
    class CreateDeviceCollection
    {
        public static void Migrate(string[] args)
        {
            LiteDatabase Db = new LiteDatabase(@"AdbMe/Database/Data/AdbMe.db");

            // Get a collection (or create, if doesn't exist)
            ILiteCollection<Device> col = Db.GetCollection<Device>("device");
            col.EnsureIndex(x => x.Primary, true);
            col.EnsureIndex(x => x.Name, true);

            // Query the collection
            List<Device> results = col.Query()
                .OrderBy(x => x.Primary)
                .Limit(1)
                .ToList();
        }
    }
}