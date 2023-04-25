using ServiceStack;

namespace AdbMe.ServiceModel
{
    [Route("/api/hello")]
    [Route("/api/hello/{Serial}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Serial { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

    [Route("/api/hello2")]
    [Route("/api/hello2/{Name}")]
    public class Hello2 : IReturn<HelloResponse2>
    {
        public string Name { get; set; }
    }

    public class HelloResponse2
    {
        public string Result { get; set; }
    }
}
