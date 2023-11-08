using HITSBackEnd.DataBaseContext;
using HITSBackEnd.Models.AccountModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace HITSBackEnd.AdditionalTasks
{
    public class TokenBlackListCleaner: IHostedService
    {
        private readonly AppDbContext _db;
        private Timer _timer;

        public TokenBlackListCleaner(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoCleanup, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoCleanup(object state)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var TokensToDelete = _db.BlackListTokens
                .Where(t => (tokenHandler.ReadToken(t.Token)).ValidTo >= DateTime.UtcNow).ToList();
            _db.BlackListTokens.RemoveRange(TokensToDelete);
        }
    }
}
