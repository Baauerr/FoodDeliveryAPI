namespace HITSBackEnd.Services.Orders
{
    public class TimeChecker
    {

        private readonly IConfiguration _configuration;

        public TimeChecker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidTime(DateTime orderTime, DateTime deliveryTime)
        {
            var minDeliveryTime = double.Parse(_configuration["Time:DifferenceBetweenOrderAndDelivery"]);
            TimeSpan timeDDifference = deliveryTime - orderTime;
            return (timeDDifference.TotalHours > minDeliveryTime);
        }
    }
}
