using System.ComponentModel.DataAnnotations;
using WebServer.MvcFramework.Users;

namespace BattleCards.Data.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Role = IdentityRole.User;
            this.Cards = new HashSet<Card>();
        }        

        public virtual ICollection<Card> Cards { get; set; }
    }
}
