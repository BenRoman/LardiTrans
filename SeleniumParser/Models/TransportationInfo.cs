using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetrieveAndSetDataFromQueue.Models
{
    public class TransportationInfo
    {
        [BsonId]
        public long transId { get; set; }
        public int amountOfLikes { get; set; }
        public string loadingDate { get; set; }
        public string vehicleType { get; set; }
        public string cargoDescription { get; set; }
        public string paymentType { get; set; }
        public string routFrom { get; set; }
        public string routTo { get; set; }
        public string routFromCountry { get; set; }
        public string routToCountry { get; set; }

    }



}