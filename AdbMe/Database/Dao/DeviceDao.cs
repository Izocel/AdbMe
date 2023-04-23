using AdbMe.Models;
using LiteDB;

namespace Database.Dao
{
    sealed class DeviceDao : BaseDao<Device>
    {
        public DeviceDao() : base("device") { }
    }

}