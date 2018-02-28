using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ReactChatDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "React Chat Demo";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
