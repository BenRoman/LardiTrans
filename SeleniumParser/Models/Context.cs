using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetrieveAndSetDataFromQueue.Models
{
    public class TransContext
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
       
    }
}