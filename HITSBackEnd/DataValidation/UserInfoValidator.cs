using System.Text.RegularExpressions;

namespace HITSBackEnd.DataValidation
{
    public class UserInfoValidator
    {

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            const string pattern = @"^(?:\+7\d{10}|\b8\d{10}\b)$";

            var isValid = Regex.IsMatch(phoneNumber, pattern);

            return isValid;
        }
        public static string ValidatePassword(string password)
        {
            if (password.Length < 6)
            {
                return "Пароль должен быть длиннее 8 символов";
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return "В пароле должны быть строчные буквы";
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return "В пароле должны быть заглавные буквы";
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                return "В пароле должны быть цифры";
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*()]"))
            {
                return "В пароле должны быть специальные символы (!@#$%^&*())";
            }

            return "";
        }
        public static bool ValidateBirthDate(DateTime birthDate)
        {
            var minDate = DateTime.UtcNow.AddYears(-130);
            var maxDate = DateTime.UtcNow;
            return birthDate <= maxDate && birthDate >= minDate;
        }

        public static bool ValidateEmail(string email)
        {
            const string pattern = @"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)";

            var isValid = Regex.IsMatch(email, pattern);

            return isValid;
        }
    }
}

