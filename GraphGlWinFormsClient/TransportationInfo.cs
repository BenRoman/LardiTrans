using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParse.Models
{
    public class TransportationInfo
    {
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
