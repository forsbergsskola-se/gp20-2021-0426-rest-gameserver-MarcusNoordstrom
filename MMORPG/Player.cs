using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG {
    public class Player {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsDeleted { get; set; }
        
        public DateTime CreationTime() {
            return Id.CreationTime.ToLocalTime();
        }

        public static async Task<Player> CreateNewPlayer(string name) {
            Database db = new Database();
            Player player = new Player {Name = name, Level = 1};
            await db.Create(player);
            return player;
        }
    }
}