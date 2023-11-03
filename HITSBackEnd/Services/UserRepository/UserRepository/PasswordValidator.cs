using Microsoft.AspNetCore.Identity;

namespace HITSBackEnd.Services.Account.IRepository
{
    public class PasswordValidatoring
    {
        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            bool succesCondition = passwordHasher.VerifyHashedPassword("2023", hashedPassword, providedPassword) == (PasswordVerificationResult.Success);
            bool oldHashCondition = passwordHasher.VerifyHashedPassword("2023", hashedPassword, providedPassword) == (PasswordVerificationResult.SuccessRehashNeeded);

            if (succesCondition || oldHashCondition)
                return true;
            return false;
        }
    }
}
