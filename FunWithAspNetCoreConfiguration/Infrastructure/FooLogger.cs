using System;

namespace FunWithAspNetCoreConfiguration.Infrastructure
{
    public class FooLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}