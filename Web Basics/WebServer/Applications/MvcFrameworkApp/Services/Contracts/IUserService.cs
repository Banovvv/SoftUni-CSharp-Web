namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> IsEmailAvailable(string email);
        Task<bool> IsUsernameAvailable(string username);
        Task<string> GetUserIdAsync(string username, string password);
        Task CreateUserAsync(string username, string email, string password);
    }
}
