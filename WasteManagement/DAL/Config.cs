using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Config
    {
        public static DataBaseType dataBaseType = DataBaseType.SqlClient;
        //public static string con = "Data Source=localhost;Initial Catalog=WasteManagement;User ID=sa;Password=123456";
        public static string con = "Data Source=localhost;Initial Catalog=WasteManagement;User ID=sa;Password=jy.sa.123";
        //public static string con = "Data Source=192.168.1.85;Initial Catalog=WasteManagement;User ID=sa;Password=sasa.123";
        public static string conn2 = "Data Source=LocalHost;Initial Catalog=OnLineMonitorTest;User ID=sa;Password=123456";
        public static string conn3 = "Data Source=192.168.1.84;Initial Catalog=zjcy_jxhb;User ID=sa;Password=cyserver.sa.84";
        public static string conn4 = "Data Source=192.168.1.100;Initial Catalog=OnLineMonitor;User ID=sa;Password=hgdao";
        public static string conn5 = "Data Source=192.168.1.84;Initial Catalog=sc_zlkz;User ID=sa;Password=cyserver.sa.84";
        public static string conn6 = "Data Source=LocalHost;Initial Catalog=sc_zlkz;User ID=sa;Password=123456";
    }
}

