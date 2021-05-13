using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MMORPG {
    public class Database : IRepository {

        public static void MongoDatabase() {
            var client = new MongoClient("mongodb://localhost:27017").GetDatabase("Game");

            var collection = client.GetCollection<Player>("Players");

            var filter = Builders<Player>.Filter.Empty;
            var cl = collection.Find(filter).ToList();

            foreach (var VARIABLE in cl) {
                Console.WriteLine(VARIABLE.Name);
            }
        }
        
        public Task<Player> Get(Guid id) {
            
            throw new NotImplementedException();
             
        }

        public Task<Player[]> GetAll() {
            throw new NotImplementedException();
        }

        public Task<Player> Create(Player player) {
            throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player) {
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id) {
            throw new NotImplementedException();
        }
    }
}