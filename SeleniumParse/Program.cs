using GraphQL;
using GraphQL.Types;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RabbitMQ.Client;
using RetrieveAndSetDataFromQueue.Models;
using SeleniumParse.Models;
using SeleniumParser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationInfo = RetrieveAndSetDataFromQueue.Models.TransportationInfo;

namespace SeleniumParse
{
    class Program
    {
        public static IWebDriver driver;

        public static List<TransportationInfo> FetchDataFromWebSite()
        {
            driver = new ChromeDriver();
            driver.Url = "https://lardi-trans.com/de/";

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.FindElement(By.ClassName("main-lang_select")).Click();
            driver.FindElement(By.XPath("//*[@id='main-lang']/li[2]/a")).Click();

            var destinations = driver.FindElements(By.Id("react-select-2-input"));
            destinations[0].SendKeys("Україна" + Keys.Enter);
            destinations[1].SendKeys("Львів");
            Task.Delay(1000).Wait();
            destinations[1].SendKeys(Keys.Enter);
            driver.FindElement(By.XPath("//*[@id='main-page-search']/div/div[3]/div[1]/button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            By locator = By.Name("paginator");
            IWebElement element = driver.FindElement(locator);
            //element.Click();
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue("100");
            wait.Until(ExpectedConditions.StalenessOf(element));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));

            IList<IWebElement> records = driver.FindElements(By.ClassName("ps_search-result_data-item"))
                .Select(el => el.FindElement(By.ClassName("ps_data_wrapper"))).ToList();

            return TransportationInfoMap(records);

        }

        public static List<TransportationInfo> TransportationInfoMap(IList<IWebElement> records)
        {
            List<TransportationInfo> transportationInfos = new List<TransportationInfo>();
            foreach (var record in records)
            {
                var transId = Convert.ToInt32(record.GetAttribute("data-ps-id"));
                var loadingDate = record.GetValueByClassName("ps_data_load_date__mobile-info");
                var vehicleType = record.GetValueByClassName("ps_data_transport__mobile");
                var cargoDesc = record.GetValueByClassName("ps_data_cargo__mobile");
                var paymentType = record.GetValueByClassName("ps_data_payment__mobile");

                var routFrom = record.FindElement(By.ClassName("ps_data_rout__from")).GetValueByClassName("ps_data_rout_item");
                var routTo = record.FindElement(By.ClassName("ps_data_rout__to")).GetValueByClassName("ps_data_rout_item");
                var bothCountries = record.FindElement(By.ClassName("ps_data_direction")).GetInnerHtml().Split('-');
                var routFromCountry = bothCountries[0];
                var routToCountry = bothCountries[1];
                transportationInfos.Add(new TransportationInfo(transId, loadingDate, vehicleType, cargoDesc, paymentType, routFrom, routTo, routFromCountry, routToCountry));
            }

            driver.Close();
            return transportationInfos;
        }

        public static void RabbitMQSet(List<TransportationInfo> infos)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare("TranspotrationQueue", true, false, false);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(infos));
                    //var body = Encoding.UTF8.GetBytes("Hello world ");
                    model.BasicPublish(String.Empty, "TranspotrationQueue", body: body);
                }
            }
        }

        public async static Task loopWithTimer(TimeSpan timeSpan)
        {
            //List<TransportationInfo> items = new List<TransportationInfo>();
            List<TransportationInfo> items = FetchDataFromWebSite();
            //RabbitMQSet(items);
            TransContext ctx = new TransContext();
            ctx.SetMany(items);
            await Task.Delay(timeSpan);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                loopWithTimer(TimeSpan.FromMinutes(60)).Wait();
            }
        }
    }
}
