namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        bool IsValidEmail(string email);
        Task<bool> IsEmailAvailableAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<string> GetUserIdAsync(string username, string password);
        Task<string> CreateUserAsync(string username, string email, string password);
    }
}
