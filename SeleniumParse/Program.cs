using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RabbitMQ.Client;
using SeleniumParse.Models;
using SeleniumParser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParse
{
    class Program
    {
        public static List<TransportationInfo> FetchDataFromWebSite()
        {
            IWebDriver driver = new ChromeDriver();
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




            IList<IWebElement> record22s = driver.FindElements(By.ClassName("ps_search-result_data-item"));
            IList<IWebElement> records = driver.FindElements(By.ClassName("ps_search-result_data-item"))
                .Select(el => el.FindElement(By.ClassName("ps_data_wrapper"))).ToList();

            return TransportationInfoMap(records);

        }

        public static List<TransportationInfo> TransportationInfoMap(IList<IWebElement> records)
        {
            List<TransportationInfo> transportationInfos = new List<TransportationInfo>();
            foreach (var record in records)
            {
                //var loadingDate = record.FindElement(By.ClassName("ps_data_load_date__mobile-info"))
                //    .FindElement(By.TagName("span")).Text;
                //var vehicleType = record.
                //var rr = record.FindElements(By.ClassName("ps_data_load_date"));
                //var rr1 = record.FindElements(By.ClassName("ps_data_load_date__mobile-info"));
                var loadingDate = record.GetValueByClassName("ps_data_load_date__mobile-info");
                var vehicleType = record.GetValueByClassName("ps_data_transport__mobile");
                var cargoDesc = record.GetValueByClassName("ps_data_cargo__mobile");
                var paymentType = record.GetValueByClassName("ps_data_payment__mobile");

                var routFrom = record.FindElement(By.ClassName("ps_data_rout__from")).GetValueByClassName("ps_data_rout_item");
                var routTo = record.FindElement(By.ClassName("ps_data_rout__to")).GetValueByClassName("ps_data_rout_item");
                var bothCountries = record.FindElement(By.ClassName("ps_data_direction")).GetInnerHtml().Split('-');
                var routFromCountry = bothCountries[0];
                var routToCountry = bothCountries[1];
                transportationInfos.Add(new TransportationInfo(loadingDate, vehicleType, cargoDesc, paymentType, routFrom, routTo, routFromCountry, routToCountry));
            }
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
                    model.BasicPublish(String.Empty, "TranspotrationQueue", body: body);
                }
            }
        }

        static void Main(string[] args)
        {
            List<TransportationInfo> items = FetchDataFromWebSite();

            RabbitMQSet(items);



            //SaveDocs().GetAwaiter().GetResult();

            Console.ReadLine();


        }

        private static async Task SaveDocs()
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("transInfo");
            var collection = database.GetCollection<Person>("infos");
            Person person1 = new Person
            {
                Name = "Jack",
                Age = 29,
                Languages = new List<string> { "english", "german" },
                Company = new Company
                {
                    Name = "Google"
                }
            };
            await collection.InsertOneAsync(person1);
        }
    }
    class Person
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }
        public List<string> Languages { get; set; }
    }
    class Company
    {
        public string Name { get; set; }
    }
}
