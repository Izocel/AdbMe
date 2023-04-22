using Migration.v100;

namespace Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! 00000");

            CreateDeviceCollection.Migrate(args);
        }
    }
}