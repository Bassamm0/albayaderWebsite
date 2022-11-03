

namespace AlbayaderWeb
{
    public static class UtilityHelper
    {
    
        public static DateTime convertUTCtoTimeZone(DateTime timeUtc, string timezone)
        {

            DateTime cstTime= new DateTime();

            if (timezone == "Asia/Dubai")
            {
                timezone = "Arabian Standard Time";
            }
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                 cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            }
            catch (Exception e)
            {

            }
            
           
    
                          
            return cstTime;
        }


    }
}
