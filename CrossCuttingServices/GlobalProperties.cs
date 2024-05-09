using System;

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Class containing general purpose properties intended for reference by any caller
    /// </summary>
    public static class GlobalProperties
    {
        #region DateTime properties
        /// <summary>Gets the current datetime for the P2RMIS application to use.</summary>
        /// <value>The current P2RMIS date and time.</value>
        /// <remarks>This should be used instead of DateTime.Now</remarks>
        public static DateTime P2rmisDateTimeNow
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime date = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
                return date;
            }
        }
        /// <summary>Gets the current datetime for the P2RMIS application to use with time part zero (midnight).</summary>
        /// <value>The current P2RMIS date.</value>
        /// <remarks>This should be used instead of DateTime.Today</remarks>
        public static DateTime P2rmisDateToday
        {
            get
            {
                return P2rmisDateTimeNow.Date;
            }
        }
        #endregion
    }
}
