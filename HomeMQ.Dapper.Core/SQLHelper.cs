using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HomeMQ.DapperCore
{
    public static class SQLHelper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;

        }
        //public static string ConnectionString = @"Data Source=DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = DemoDB2; User ID = master; Password=control;Connect Timeout = 30;";
        public static string ConnectionString = @"Data Source=DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = DemoDB2; uid = master; pwd=control; timeout= 100000;";
    }
}
