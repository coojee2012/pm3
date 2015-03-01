using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectData.ChangeDB
{
   public class ChangeDB : PrjChangeDataContext
    {
        public ChangeDB()
            : base(CreateConnection())
        {

        }

        private static string CreateConnection()
        {
            string access = System.Configuration.ConfigurationManager.ConnectionStrings["PrjChangeData"].ConnectionString;
            return access;
        }
    }
}
