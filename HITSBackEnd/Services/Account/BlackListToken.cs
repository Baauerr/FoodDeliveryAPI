using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HITSBackEnd.Services.Account
{
    public class BlackListToken
    {
        [Key]
        [Column(Order = 0)]
        public string userEmail { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Token { get; set; }
    }
}
