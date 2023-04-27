using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtherScan_Test.Models
{
    public class HomeModel
    {
        public List<HomeModel> DataList { get; set; }
        public int ID { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string HREF { get; set; }
        public string Symbol { get; set; }
        public string ContractAddress { get; set; }
        public decimal Price { get; set; }
        public int TotalSupply { get; set; }
        public int TotalHolders { get; set; }
        public decimal TotalSupplyPercentage { get; set; }

        public HomeModel()
        {
            DataList = new List<HomeModel>();
        }
    }
}