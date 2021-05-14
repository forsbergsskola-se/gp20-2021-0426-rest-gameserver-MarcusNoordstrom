using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;

namespace MMORPG
{
    public class Program
    {
        static async Task Main(string[] args) {
            Database db = new Database();

            var p = await db.Get(ObjectId.Parse("609dba35770604b509b323bd"));
            Console.WriteLine(p.Score);
            Console.WriteLine("Adding 2 to score");
            ModifiedPlayer p2 = new ModifiedPlayer();
            p2.Score = 2;
            await db.Modify(ObjectId.Parse("609dba35770604b509b323bd"), p2);
            var p3 = await db.Get(ObjectId.Parse("609dba35770604b509b323bd"));
            Console.WriteLine(p3.Score);
            
            // TEST TO CREATE A NEW PLAYER
            // var player = await Player.CreateNewPlayer("Trey, the lost tester");
            // Console.WriteLine($"Created {player.Name}");
            // Console.WriteLine(player.CreationTime());

            // TEST TO GET A PLAYER BY ID
            // var player = await db.Get(ObjectId.Parse("609db71dc967721fa6031f5a"));
            // Console.WriteLine(player.Id);

            // TEST TO GET ALL NOT DELETED PLAYERS
            // var b = await db.GetAll();
            // foreach (var VARIABLE in b) {
            //     Console.WriteLine(VARIABLE.Name);
            // }

            // TEST TO DELETE A PLAYER
            // await db.Delete(ObjectId.Parse("INSERT ID"));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
