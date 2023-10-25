using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using HITSBackEnd.DataBase;

namespace HITSBackEnd.Controllers.AttributeUsage
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenBlacklistFilterAttribute : ActionFilterAttribute
    {
        private readonly AppDbContext _tokenBlacklist;

        public TokenBlacklistFilterAttribute(AppDbContext tokenBlacklist)
        {
            _tokenBlacklist = tokenBlacklist;
        }

        public bool IsTokenInBlacklist(string token)
        {
            return _tokenBlacklist.blackListTokens.Any(t => t.Token == token);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (IsTokenInBlacklist(token))
            {
                context.Result = new UnauthorizedObjectResult("Токен не действителен.");
            }
            base.OnActionExecuting(context);
        }
    }
}

