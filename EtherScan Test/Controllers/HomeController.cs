using EtherScan_Test.Components;
using EtherScan_Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EtherScan_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Home();
        }

        public ActionResult Home(int ID = 0)
        {
            var Model = new HomeModel();

            decimal GrandTotalSupply = 0;

            var dsData = HomeDB.GetData(ID);

            if (dsData.Tables[1].Rows.Count > 0)
            {
                GrandTotalSupply = Convert.ToInt32(dsData.Tables[1].Rows[0]["TOTAL_SUPPLY"].ToString());
            }

            #region Get Token Detail

            Model.ID = ID;

            if (ID != 0)
            {
                if (dsData.Tables[2].Rows.Count > 0)
                {
                    Model.Name = dsData.Tables[2].Rows[0]["NAME"].ToString();
                    Model.Symbol = dsData.Tables[2].Rows[0]["SYMBOL"].ToString();
                    Model.ContractAddress = dsData.Tables[2].Rows[0]["CONTRACT_ADDRESS"].ToString();
                    Model.TotalSupply = Convert.ToInt32(dsData.Tables[2].Rows[0]["TOTAL_SUPPLY"].ToString());
                    Model.TotalHolders = Convert.ToInt32(dsData.Tables[2].Rows[0]["TOTAL_HOLDERS"].ToString());
                }
            }

            #endregion

            #region Get Token listing 

            string URL = HttpContext.Request.Url.AbsoluteUri;

            foreach (DataRow dr in dsData.Tables[0].Rows)
            {
                var HM = new HomeModel();
                HM.ID = Convert.ToInt32(dr["ID"].ToString());
                HM.Rank = 0; //Convert.ToInt32(dr["CRANKSET_NAME"].ToString());
                HM.Symbol = dr["SYMBOL"].ToString();
                HM.Name = dr["NAME"].ToString();               
                HM.HREF = URL + "Home/DetailPage?ID=" + dr["SYMBOL"].ToString();
                HM.ContractAddress = dr["CONTRACT_ADDRESS"].ToString();
                HM.TotalSupply = Convert.ToInt32(dr["TOTAL_SUPPLY"].ToString());
                HM.TotalHolders = Convert.ToInt32(dr["TOTAL_HOLDERS"].ToString());
                HM.TotalSupplyPercentage = decimal.Parse(HM.TotalSupply.ToString()) / GrandTotalSupply * 100;
                Model.DataList.Add(HM);
            }

            #endregion

            #region Get Pie Chart Data 
            string Label = "";
            string TotalSupply = "";
            foreach (DataRow dr in dsData.Tables[3].Rows)
            {
                Label = Label + "'" + dr["NAME"].ToString() + "',";
                TotalSupply = TotalSupply + dr["TOTALSUPPLY"].ToString() + ",";
            }

            Label = Label.Remove(Label.Length - 1, 1);
            Label = Label.Replace("\"", "");

            TotalSupply = TotalSupply.Remove(TotalSupply.Length - 1, 1);
            TotalSupply = TotalSupply.Replace("\"", "");

            ViewBag.Label = Label;
            ViewBag.TotalSupply = TotalSupply;
            #endregion

            return View("Home", Model);
        }

        [HttpPost]
        public ActionResult SaveMethod(int ID, string Name, string Symbol, string ContractAddress, int TotalSupply, int TotalHolders)
        {
            if (ID == 0)
            {
                HomeDB.InsertData(Name, Symbol, ContractAddress, TotalSupply, TotalHolders);
            }
            else
            {
                HomeDB.UpdateData(ID, Name, Symbol, ContractAddress, TotalSupply, TotalHolders);
            }

            return Home();
        }

        public ActionResult DetailPage(string ID = "")
        {
            var Model = new HomeModel();


            var dsData = HomeDB.GetDataBySymbol(ID);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                Model.Price = decimal.Parse(dsData.Tables[0].Rows[0]["PRICE"].ToString());
                Model.TotalSupply = int.Parse(dsData.Tables[0].Rows[0]["TOTAL_SUPPLY"].ToString());
                Model.TotalHolders = int.Parse(dsData.Tables[0].Rows[0]["TOTAL_HOLDERS"].ToString());
                Model.Name = dsData.Tables[0].Rows[0]["NAME"].ToString();
                Model.ContractAddress = dsData.Tables[0].Rows[0]["CONTRACT_ADDRESS"].ToString();
            }
            else
            {
                Model.Price = 0;
            }

            return View("DetailPage", Model);
        }
    }
}