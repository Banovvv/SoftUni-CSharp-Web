using BattleCards.Data;
using BattleCards.Services.Contracts;

namespace BattleCards.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDataContext context;

        public CardService(ApplicationDataContext context)
        {
            this.context = context;
        }
    }
}
