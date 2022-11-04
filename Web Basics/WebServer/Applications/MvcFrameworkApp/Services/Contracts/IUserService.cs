namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        bool IsEmailAvailable(string email);
        bool IsUsernameAvailable(string username);
        bool IsUserValid(string username, string password);
        void CreateUser(string username, string email, string password);
    }
}
