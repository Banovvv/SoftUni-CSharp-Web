using System.ComponentModel.DataAnnotations;
using WebServer.MvcFramework.Users;

namespace BattleCards.Data.Models
{
    public class User : UserIdentity<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<UserCard>();
        }        

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
