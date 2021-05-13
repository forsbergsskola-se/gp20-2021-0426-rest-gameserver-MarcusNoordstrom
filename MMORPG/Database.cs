using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG {
    public class Database : IRepository {

        static IMongoDatabase MongoDatabase() {
            var client = new MongoClient("mongodb://localhost:27017").GetDatabase("Game");
            return client;
        }

        public async Task<Player> Get(ObjectId id) {
            var collection = MongoDatabase().GetCollection<Player>("Players");
            var fieldEq = new StringFieldDefinition<Player, ObjectId>(nameof(Player.Id));
            var filter = new FilterDefinitionBuilder<Player>().Eq(fieldEq, id);
            var player = await collection.Find(filter).SingleAsync();
            return player;
        }

        public async Task<List<Player>> GetAll() {
            var collection = MongoDatabase().GetCollection<Player>("Players");
            var fieldEq = new StringFieldDefinition<Player, bool>(nameof(Player.IsDeleted));
            var filter = new FilterDefinitionBuilder<Player>().Eq(fieldEq, false);
            var playersList =  collection.Find(filter).ToListAsync();
            
            return await playersList;
        }

        public async Task<Player> Create(Player player) {
            player.Level = 1;
            var collection = MongoDatabase().GetCollection<Player>("Players");
            await collection.InsertOneAsync(player);
            return player;
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player) {
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id) {
            throw new NotImplementedException();
        }
    }
}