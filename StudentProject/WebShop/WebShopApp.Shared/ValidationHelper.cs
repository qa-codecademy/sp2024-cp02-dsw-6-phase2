using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopApp.Shared
{
    public static class ValidationHelper
    {
        public static void ValidateReqiredStrings(string value , string field , int maxNumOfChar)
        {

            if (string.IsNullOrEmpty(value))
            {
                throw new DataException($"{field} is reqired");

            }
            if (value.Length > maxNumOfChar)
            {
                throw new DataException($"{field} cant contain more than {maxNumOfChar} characters");
            }
        }
    }
}
