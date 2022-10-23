namespace BattleCards.Data.Models
{
    public class User
    {
        public User()
        {
            Cards = new HashSet<UserCard>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
