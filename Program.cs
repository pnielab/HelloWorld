using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8080/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                /*
                 * unmark to test 
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(baseAddress + "HelloWorld/bbb").Result;
                Console.WriteLine(response);
                 */
                Console.ReadLine();
            }
        }
    }
}
