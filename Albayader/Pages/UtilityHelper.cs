

namespace AlbayaderWeb
{
    public static class UtilityHelper
    {
    
        public static DateTime convertUTCtoTimeZone(DateTime theDateTime,string timezone)
        {
            if(timezone == "Asia/Dubai")
            {
                timezone = "Arabian Standard Time";
            }
            
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            theDateTime = TimeZoneInfo.ConvertTimeToUtc(theDateTime, easternZone).ToLocalTime();

            return theDateTime;
        }


    }
}
