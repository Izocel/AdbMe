namespace AdbMe.Helpers
{   
    public static class Helpers
    {
        public static bool IsIpValid(string? ip = "xxx") {
            if(ip == null) {return false;}
            System.Net.IPAddress ipAddress = null;
            return System.Net.IPAddress.TryParse(ip.Trim().Split(":")[0], out ipAddress);
        }
    }
}