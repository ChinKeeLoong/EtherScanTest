using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace UpdatePrice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start To Update Price Per Symbol");

            while (true)
            {
                var dsToken = GetAllSymbol();

                
                foreach (DataRow dr in dsToken.Tables[0].Rows)
                {
                    string Symbol = dr["SYMBOL"].ToString();

                    string Label = "System Start Checking " + Symbol.ToUpper() + " Price ...";

                    Console.WriteLine(Label);

                    var request = (HttpWebRequest)WebRequest.Create(string.Format("https://min-api.cryptocompare.com/data/price?fsym={0}&tsyms=USD", Symbol.ToUpper()));
                    var encoding = new ASCIIEncoding();
                    byte[] data = encoding.GetBytes(string.Empty);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var result = JObject.Parse(responseString);
                    var Price = result.GetValue("USD").ToString();

                    UpdateData(Symbol, decimal.Parse(Price));

                    Console.WriteLine("Price : " + Price);
                }

                System.Threading.Thread.Sleep(3000000);

            }
        }

        private static DataSet GetAllSymbol()
        {
            DataSet ds = new DataSet();
            using (SqlConnection sqlConn = DBOperator.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand sqlComm = new SqlCommand("SP_GetAllSymbol", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;               

                da.SelectCommand = sqlComm;
                sqlConn.Open();
                da.Fill(ds);
                sqlConn.Close();
            }

            return ds;
        }

        private static void UpdateData(string Symbol, decimal Price)
        {
            SqlConnection sqlConn = DBOperator.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdatePrice", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pSymbol = sqlComm.Parameters.Add("@Symbol", SqlDbType.NVarChar, 5);
            pSymbol.Direction = ParameterDirection.Input;
            pSymbol.Value = Symbol;

            SqlParameter pPrice = sqlComm.Parameters.Add("@Price", SqlDbType.Decimal);
            pPrice.Direction = ParameterDirection.Input;
            pPrice.Value = Price;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
    }
}
