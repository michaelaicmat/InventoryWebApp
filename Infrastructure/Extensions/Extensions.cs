using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static double ToLong(this string str)
        {
            if(!double.TryParse(str, out double val))
            {
                throw new Exception($"Cannot convert the string {str} to a float");
            }

            return val;
        }
    }
}