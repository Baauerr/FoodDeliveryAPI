namespace HITSBackEnd.DataValidation
{
    public class DeliveryTimeChecker
    {

        private readonly IConfiguration _configuration;

        public DeliveryTimeChecker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidTime(DateTime deliveryTime)
        {
            var orderTime = DateTime.Now;
            var minDeliveryTime = double.Parse(_configuration.GetValue<string>("DeliveryTime:DifferenceBetweenOrderAndDelivery"));
            TimeSpan timeDDifference = deliveryTime - orderTime;
            return timeDDifference.TotalHours > minDeliveryTime;
        }
    }
}
