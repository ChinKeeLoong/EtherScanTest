using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

public class DBOperator
{

    public static SqlConnection GetConnection()
    {
        string localConnectionString = @"Data Source = CKLOONG\MSSQLSERVER2019; Initial Catalog = EtherScan; Integrated Security = true; Connect Timeout = 30000";
        return new SqlConnection(localConnectionString);
    }

}


