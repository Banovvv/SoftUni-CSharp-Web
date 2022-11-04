using BattleCards.Data;
using BattleCards.Services.Contracts;

namespace BattleCards.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDataContext context;

        public UserService()
        {
            this.context = new ApplicationDataContext();
        }

        public void CreateUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUserValid(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.context.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.context.Users.Any(x => x.Username == username);
        }
    }
}
