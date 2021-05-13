using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MMORPG
{
    public class Program
    {
        static async Task Main(string[] args) {
            Database db = new Database();
            
            //TEST TO GET A PLAYER BY ID
            // var player = await db.Get(ObjectId.Parse("609d9656edc14209e038c32f"));
            // Console.WriteLine(player.Id);
            
            //TEST TO GET ALL NOT DELETED PLAYERS
            // var b = await db.GetAll();
            // foreach (var VARIABLE in b) {
            //     Console.WriteLine(VARIABLE.Name);
            // }
            
            //TEST TO CREATE A NEW PLAYER
            Player playerToCreate = new Player {Name = "Leif The Arrogant Goblin"};
            var playerMade = await db.Create(playerToCreate);
            Console.WriteLine(playerMade.Name);
            Console.WriteLine(playerMade.CreationTime());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
