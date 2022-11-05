namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> IsEmailAvailableAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<string> GetUserIdAsync(string username, string password);
        Task CreateUserAsync(string username, string email, string password);
    }
}
