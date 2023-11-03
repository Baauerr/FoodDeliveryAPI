namespace HITSBackEnd.Services.Orders
{
    public class TimeChecker
    {

        private const double TargetHourDifference = 1;
        public static bool ValidTime(DateTime orderTime, DateTime deliveryTime)
        {
            TimeSpan timeDDifference = deliveryTime - orderTime;
            return (timeDDifference.TotalHours > TargetHourDifference);
        }
    }
}
