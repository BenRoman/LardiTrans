using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RetrieveAndSetDataFromQueue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RetrieveAndSetDataFromQueue.Controllers
{
    public class HomeController : Controller
    {

        public List<TransportationInfo> transInfo = new List<TransportationInfo>();
        private readonly TransContext db = new TransContext();


        public HomeController()
        {
            //List<TransportationInfo> items = db.Transportations.Find(new BsonDocument()).ToList();
            List<TransportationInfo> items = new List<TransportationInfo>();

            if (items.Count > 0)
            {
                this.transInfo = items;
            }
            else
            {
                retrieveDataFromTheQueue();
                db.SetMany(this.transInfo);
            }
        }

        public ActionResult Index()
        {
            return View(this.transInfo);
        }
        

        public void retrieveDataFromTheQueue()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var received = false;

            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare("TranspotrationQueue", true, false, false);

                    var consumer = new EventingBasicConsumer(model);

                    consumer.Received += (eventModel, args) =>
                    {
                        string jsonStr = Encoding.UTF8.GetString(args.Body.ToArray());
                        List<TransportationInfo> transInfos = JsonConvert.DeserializeObject<List<TransportationInfo>>(jsonStr);
                        this.transInfo = transInfos;
                        received = true;

                    };

                    model.BasicConsume("TranspotrationQueue", true, consumer);
                    while (!received)
                    {
                        Task.Delay(0);
                    }
                }
            }

        }

    }
}