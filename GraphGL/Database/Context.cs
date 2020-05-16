using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GraphGL.Database
{
    public class TransContext: DbContext
    {
        IMongoDatabase database; 

        public TransContext()
        {
            string connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            database = client.GetDatabase("transInfo");

        }
        public IMongoCollection<TransportationInfo> Transportations
        {
            get { return database.GetCollection<TransportationInfo>("infos"); }
        }
        
        public async Task<TransportationInfo> GeTrans(string id)
        {
            return await Transportations.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        
        public void SetMany(List<TransportationInfo> c)
        {
            Transportations.InsertMany(c);
        }
        
        public async Task Remove(string id)
        {
            await Transportations.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public string editLikes(long id, int amountOfLikes)
        {
            var result = Transportations.UpdateOne(
                new BsonDocument("_id", id),
                new BsonDocument("$set", new BsonDocument("amountOfLikes", amountOfLikes)));
            return $"знайдено: {result.MatchedCount}; оновлено: {result.ModifiedCount}";
        }
       
    }
}