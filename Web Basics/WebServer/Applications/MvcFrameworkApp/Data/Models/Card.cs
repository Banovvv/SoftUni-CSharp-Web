using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data.Models
{
    public class Card
    {
        public Card()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Keyword { get; set; }
        [Required]
        public int Attack { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
