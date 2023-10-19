using System.Text.RegularExpressions;

namespace HITSBackEnd.Services
{
    public class DataValidator
    {
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            
            string pattern = @"^(?:\+7\d{11}|\b8\d{10}\b)$";

            Regex regex = new Regex(pattern);

            bool isValid = regex.IsMatch(phoneNumber);

            return isValid;
        }
    }
}
