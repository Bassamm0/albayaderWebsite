

namespace AlbayaderWeb
{
    public static class UtilityHelper
    {
    
        public static DateTime convertUTCtoTimeZone(DateTime timeUtc, string timezone)
        {
            if(timezone == "Asia/Dubai")
            {
                timezone = "Arabian Standard Time";
            }
            
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
    
                          
            return cstTime;
        }


    }
}
