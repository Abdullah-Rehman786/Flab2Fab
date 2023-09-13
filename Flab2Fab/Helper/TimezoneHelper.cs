using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flab2Fab.Helper
{
    public class TimezoneHelper
    {
        public static DateTime GetCentralTimeNow()
        {
            if(Environment.GetEnvironmentVariable("ON_AZURE") != null && Environment.GetEnvironmentVariable("ON_AZURE").Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                TimeZoneInfo centralTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, centralTimeZoneInfo);
            }
            else
            {
                return DateTime.Now;
            }
        }     
    }
}