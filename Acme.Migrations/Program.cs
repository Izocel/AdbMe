using Migration.v100;

namespace Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("v1.0.0 - Migration Start");
            CreateDeviceCollection.Migrate(args);

            Console.WriteLine("v1.0.0 - Migration Stop");
        }
    }
}