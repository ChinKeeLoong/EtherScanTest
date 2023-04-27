using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EtherScan_Test.Components
{
    public class HomeDB
    {
        public static DataSet GetData(int ID)
        {
            SqlConnection sqlConn = DBOperator.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetData", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pID = sqlComm.Parameters.Add("@ID", SqlDbType.Int);
            pID.Direction = ParameterDirection.Input;
            pID.Value = ID;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);
            sqlConn.Close();

            return ds;
        }
        public static DataSet GetDataBySymbol(string ID)
        {
            SqlConnection sqlConn = DBOperator.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetDataBySymbol", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pID = sqlComm.Parameters.Add("@ID", SqlDbType.NVarChar,50);
            pID.Direction = ParameterDirection.Input;
            pID.Value = ID;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);
            sqlConn.Close();

            return ds;
        }
        public static void InsertData(string Name, string Symbol, string ContractAddress, int TotalSupply, int TotalHolders )
        {
            SqlConnection sqlConn = DBOperator.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_InsertData", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pName = sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            pName.Direction = ParameterDirection.Input;
            pName.Value = Name;

            SqlParameter pSymbol = sqlComm.Parameters.Add("@Symbol", SqlDbType.NVarChar, 5);
            pSymbol.Direction = ParameterDirection.Input;
            pSymbol.Value = Symbol;

            SqlParameter pContractAddress = sqlComm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 66);
            pContractAddress.Direction = ParameterDirection.Input;
            pContractAddress.Value = ContractAddress;

            SqlParameter pTotalSupply = sqlComm.Parameters.Add("@TotalSupply", SqlDbType.Int);
            pTotalSupply.Direction = ParameterDirection.Input;
            pTotalSupply.Value = TotalSupply;

            SqlParameter pTotalHolders = sqlComm.Parameters.Add("@TotalHolders", SqlDbType.Int);
            pTotalHolders.Direction = ParameterDirection.Input;
            pTotalHolders.Value = TotalHolders;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
        public static void UpdateData(int ID,string Name, string Symbol, string ContractAddress, int TotalSupply, int TotalHolders)
        {
            SqlConnection sqlConn = DBOperator.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateData", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pID = sqlComm.Parameters.Add("@ID", SqlDbType.Int);
            pID.Direction = ParameterDirection.Input;
            pID.Value = ID;

            SqlParameter pName = sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            pName.Direction = ParameterDirection.Input;
            pName.Value = Name;

            SqlParameter pSymbol = sqlComm.Parameters.Add("@Symbol", SqlDbType.NVarChar, 5);
            pSymbol.Direction = ParameterDirection.Input;
            pSymbol.Value = Symbol;

            SqlParameter pContractAddress = sqlComm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 66);
            pContractAddress.Direction = ParameterDirection.Input;
            pContractAddress.Value = ContractAddress;

            SqlParameter pTotalSupply = sqlComm.Parameters.Add("@TotalSupply", SqlDbType.Int);
            pTotalSupply.Direction = ParameterDirection.Input;
            pTotalSupply.Value = TotalSupply;

            SqlParameter pTotalHolders = sqlComm.Parameters.Add("@TotalHolders", SqlDbType.Int);
            pTotalHolders.Direction = ParameterDirection.Input;
            pTotalHolders.Value = TotalHolders;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
    }
}