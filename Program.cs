using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = SetupConfiguration();

            System.Console.WriteLine("Configuration Manager");
            string spliter = "===============================\r\n";
            System.Console.WriteLine(spliter);

            // Run through all keys
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine("Key: {0} = {1}", item.Key, item.Value);
            }

        }

        private static IConfigurationRoot SetupConfiguration() {
            
            var builder = new ConfigurationBuilder();
            var env = PlatformServices.Default.Application;

            // Configuration can come from being In Memory or from File
            builder.SetBasePath(System.IO.Directory.GetCurrentDirectory());

            builder.AddInMemoryCollection(); // Memory Configuration
            builder.AddJsonFile("appSettings.json");
            builder.AddJsonFile($"appSettings.{System.Environment.GetEnvironmentVariable("EnvironmentName")}.json", optional: true);

            var config = builder.Build();


            config["someKey"] = "someValue";

            return config;
        }
    }
}
