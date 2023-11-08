using HITSBackEnd.DataBaseContext;
using HITSBackEnd.Models.AccountModels;

namespace HITSBackEnd.AdditionalTasks
{
    public class TokenBlackListCleaner
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

        private void DoCleanup(object state)
        {
          //Тут будет жосская очистка токенов
        }
    }
}
