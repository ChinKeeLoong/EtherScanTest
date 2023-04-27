using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

public class DBOperator
{    
    public static SqlConnection GetConnection()
    {       
        string localConnectionString = @"Data Source = CKLOONG\MSSQLSERVER2019; Initial Catalog = EtherScan; Integrated Security = true; Connect Timeout = 30000";
        return new SqlConnection(localConnectionString);
    }
}