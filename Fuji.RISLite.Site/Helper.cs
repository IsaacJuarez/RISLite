using System;
using System.Configuration;

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