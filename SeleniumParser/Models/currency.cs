using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetrieveAndSetDataFromQueue.Models
{
    public class Currency
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public double buy { get; set; }
        public double sale { get; set; }
    }
}