using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.ModelBinding;
using System;
using System.Linq;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = "http://127.0.0.1:8000";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine("Nancy Server listening on");
                Console.ReadLine();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            Get("/", args =>
            {
                return "Hello World";
            });

            Get("jsontest", args =>
            {
                var test = new
                {
                    Name = "Peter Shaw",
                    Twitter = "shawty_ds",
                    Occupation = "Software Developer"
                };

                return Response.AsJson(test);
            });

            Post("posttest", args =>
            {
                var myPerson = this.Bind<Person>();
                return String.Format("Hello there {0} with twitter handle {1} who works as a {2} count of {3} ", myPerson.Name, myPerson.Twitter, myPerson.Occupation, myPerson.Count + 5);
            });



        }
    }
}
