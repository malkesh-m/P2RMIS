using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.EntityChanges
{
    public class LookupConverterFactory
    {
        public static ILookupConverter Make(string tableName)
        {
            ILookupConverter result = null;

            if (tableName == typeof(UserInfo).Name)
            {
                result = new ProfessionalAffilationLookupConverter();
            }
            else if (tableName == typeof(UserAddress).Name)
            {
                result = new StateLookupConverter();
            }
            else if (tableName == typeof(UserDegree).Name)
            {
                result = new DegreeLookupConverter();
            }
            return result;
        }
    }


}
