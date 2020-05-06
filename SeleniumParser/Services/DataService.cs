using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RetrieveAndSetDataFromQueue.Controllers;
using RetrieveAndSetDataFromQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RetrieveAndSetDataFromQueue.Services
{
    //public class DataService
    //{
    //    public List<TransportationInfo> transInfo = new List<TransportationInfo>();
    //    private readonly TransContext db = new TransContext();

    //    public string gg;
    //    public DataService()
    //    {

    //        this.subscribeIntoDataFromTheQueue();
    //        //if (this.transInfo.Count == 0)
    //        //{
    //        //    this.transInfo = getFromDB().Result;
    //        //}
    //    }

       

    //    public void subscribeIntoDataFromTheQueue()
    //    {
    //        var factory = new ConnectionFactory()
    //        {
    //            HostName = "localhost",
    //            Port = 5672,
    //            UserName = "guest",
    //            Password = "guest"
    //        };


    //        var connection = factory.CreateConnection();
    //        var model = connection.CreateModel();
    //        model.QueueDeclare("TranspotrationQueue", true, false, false);

    //        var consumer = new EventingBasicConsumer(model);

    //        consumer.Received += (eventModel, args) =>
    //        {
    //            //string res = Encoding.UTF8.GetString(args.Body.ToArray());

    //            string jsonStr = Encoding.UTF8.GetString(args.Body.ToArray());
    //            List<TransportationInfo> transInfos = JsonConvert.DeserializeObject<List<TransportationInfo>>(jsonStr);
    //            this.transInfo = transInfos;
    //            db.SetMany(transInfo);
    //        };

    //        model.BasicConsume("TranspotrationQueue", true, consumer);

    //    }

    //}

    public sealed class DataService
    {
        public List<TransportationInfo> transInfo;
        private TransContext db;

        private static DataService instance = null;

        public static DataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataService();
                }
                return instance;
            }
        }

        private DataService()
        {
            transInfo = new List<TransportationInfo>();
            db = new TransContext();
            subscribeIntoDataFromTheQueue();

        }

        public async Task<List<TransportationInfo>> getFromDB()
        {
            return await db.Transportations.Find(new BsonDocument()).ToListAsync();
        }

        private  void subscribeIntoDataFromTheQueue()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };


            var connection = factory.CreateConnection();
            var model = connection.CreateModel();
            model.QueueDeclare("TranspotrationQueue", true, false, false);

            var consumer = new EventingBasicConsumer(model);

            consumer.Received += (eventModel, args) =>
            {
                //string res = Encoding.UTF8.GetString(args.Body.ToArray());

                string jsonStr = Encoding.UTF8.GetString(args.Body.ToArray());
                List<TransportationInfo> transInfos = JsonConvert.DeserializeObject<List<TransportationInfo>>(jsonStr);
                this.transInfo = transInfos;
                this.db.SetMany(this.transInfo);
            };

            model.BasicConsume("TranspotrationQueue", true, consumer);

        }

    }
}