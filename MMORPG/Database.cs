using System;
using System.Collections.Generic;
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

        public async Task<Player> Modify(ObjectId id, ModifiedPlayer player) {
            var collection = MongoDatabase().GetCollection<Player>("Players");
            var fieldEq = new StringFieldDefinition<Player, ObjectId>(nameof(Player.Id));
            var filter = new FilterDefinitionBuilder<Player>().Eq(fieldEq, id);
            var playerFound = await collection.Find(filter).SingleAsync();
            playerFound.Score += player.Score;
            await collection.ReplaceOneAsync(filter, playerFound);
            return playerFound;
        }

        public async Task<Player> Delete(ObjectId id) {
            var collection = MongoDatabase().GetCollection<Player>("Players");
            var fieldEq = new StringFieldDefinition<Player, ObjectId>(nameof(Player.Id));
            var filter = new FilterDefinitionBuilder<Player>().Eq(fieldEq, id);
            var player = await collection.Find(filter).SingleAsync();
            await collection.DeleteOneAsync(filter);
            return player;
        }
    }
}