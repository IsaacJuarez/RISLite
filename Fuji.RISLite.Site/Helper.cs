using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site
{
    public static class Helper
    {
        public static string ConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["RISLiteEntities"].ConnectionString;
            }
            catch (Exception ehp)
            {
                throw ehp;
            }
        }

    }
}