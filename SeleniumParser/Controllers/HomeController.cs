using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RetrieveAndSetDataFromQueue.Models;
using RetrieveAndSetDataFromQueue.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RetrieveAndSetDataFromQueue.Controllers
{

    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private Task<HttpResponseMessage> currencyState;
        private List<TransportationInfo> transportationInfos;

        public HomeController()
        {
            HttpClient client = new HttpClient();
            currencyState = client.GetAsync("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");
            this.transportationInfos = DataService.Instance.transInfo;
        }
       
        public ActionResult Index()
        {
            
            var currencyList = JsonConvert.DeserializeObject<List<Currency>>(this.currencyState.Result.Content.ReadAsStringAsync().Result);
            return View(currencyList);
        }
        [Route("list")]
        public ActionResult AllTransportations()
        {
            return View(this.transportationInfos);
        }
    }
}