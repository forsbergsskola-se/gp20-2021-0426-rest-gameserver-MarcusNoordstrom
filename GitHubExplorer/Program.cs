using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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
            Console.WriteLine("2. Search for a Username and get all their repositories");

            int.TryParse(Console.ReadLine(), out var input);
            
            switch (input) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Type in the username you want to search for");
                    Task.WaitAll(SearchForUser(Console.ReadLine()));
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Type in a username and get all their repositories");
                    Task.WaitAll(SearchForRepos(Console.ReadLine()));
                    break;
            }
        }
        
        public static async Task SearchForRepos(string userName)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = SecretValidator.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync($"/users/{userName}/repos");
            
            StreamReader sr = new StreamReader(response.Content.ReadAsStream());
            
            var repoJson = await sr.ReadToEndAsync();
            sr.Close();
            client.Dispose();

            var reposDes  = JsonSerializer.Deserialize<JsonElement>(repoJson);
            var repos = new RepoResponse(reposDes);

            for (int i = 0; i < repos.names.Count; i++) {
                Console.WriteLine($"Name: {repos.names[i]}");
                Console.WriteLine($"Description: {repos.descriptions[i]}");
                Console.WriteLine($"Url: {repos.urls[i]}");
                Console.WriteLine();
            }

            Console.WriteLine("------------");
            Console.WriteLine("Any. Go back to the main menu");
            Console.WriteLine("2. Go to user profile");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForUser(userName));
            }
            else {
                Menu();
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
            
            var usrJson = await sr.ReadToEndAsync();
            sr.Close();
            client.Dispose();
            
            var user = JsonSerializer.Deserialize<UserResponse>(usrJson);

            if (user != null)
                foreach (var info in user.info) {
                    Console.WriteLine(info);
                }

            Console.WriteLine("------------");
            Console.WriteLine("Any. Go back to the main menu");
            Console.WriteLine("2. Go to users repositories");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForRepos(userName));
            }
            else {
                Menu();
            }
        }
    }
}
