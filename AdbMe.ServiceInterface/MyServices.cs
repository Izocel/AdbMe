using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Script;
using ServiceStack.DataAnnotations;
using AdbMe.ServiceModel;

namespace AdbMe.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }

        public object Any(Hello2 request)
        {
            return new HelloResponse2 { Result = $"Hello, {request.Name}!" };
        }
    }
}
