namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        bool IsEmailAvailable(string email);
        bool IsUsernameAvailable(string username);
        Task<bool> IsUserValidAsync(string username, string password);
        Task CreateUserAsync(string username, string email, string password);
    }
}
