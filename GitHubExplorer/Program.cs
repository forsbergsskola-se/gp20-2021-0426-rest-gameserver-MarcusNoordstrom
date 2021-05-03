using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu();
        }

        static void Menu() {
            Console.Clear();
            Console.Title = "GitHub Explorer";
            Console.WriteLine("-- GitHub Explorer --");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Search for a Username");

            int.TryParse(Console.ReadLine(), out var input);
            
            switch (input) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Type in the username you want to search for");
                    Task.WaitAll(SearchForUser(Console.ReadLine()));
                    break;
            }
        }

        public static async Task SearchForUser(string userName)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = SecretValidator.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync($"/users/{userName}");
            
            StreamReader sr = new StreamReader(response.Content.ReadAsStream());
            
            var s = await sr.ReadToEndAsync();
            sr.Close();
            client.Dispose();
            
            var asd = JsonSerializer.Deserialize<UserResponse>(s);

            if (!string.IsNullOrEmpty(asd.name)) {
                Console.WriteLine($"Name: {asd.name}");    
            }
            else {
                Console.WriteLine("Name/User was not found...");
            }

            if (!string.IsNullOrEmpty(asd.company)) {
                Console.WriteLine($"Job: {asd.company}");
            }
            else {
                Console.WriteLine("Job: Unemployed");
            }
            Console.WriteLine(asd.blog);
            Console.WriteLine(asd.location);
            Console.WriteLine(asd.email);
            Console.WriteLine(asd.public_repos);
            Console.WriteLine(asd.followers);
            Console.WriteLine(asd.following);
            Console.WriteLine(asd.created_at);
            Console.WriteLine(asd.updated_at);


            Console.WriteLine("------------");
            Console.WriteLine("Press any key to go back to the menu");
            // Console.WriteLine(s);
            Console.ReadLine();
            Menu();
        }
    }
    
    
}
