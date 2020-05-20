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
        public TransportationInfo(long id, string date, string vtype, string cargodes, string payment, string from, string to, string fromCo, string toCo)
        {
            this.transId = id;
            this.loadingDate = date;
            this.vehicleType = vtype;
            this.cargoDescription = cargodes;
            this.paymentType = payment;
            this.routFrom = from;
            this.routTo = to;
            this.routFromCountry = fromCo;
            this.routToCountry = toCo;
            this.amountOfLikes = 0;
        }
    }



}