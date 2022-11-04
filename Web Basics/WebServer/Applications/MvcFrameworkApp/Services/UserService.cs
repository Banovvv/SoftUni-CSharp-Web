using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BattleCards.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDataContext context;

        public UserService()
        {
            this.context = new ApplicationDataContext();
        }

        public async Task CreateUserAsync(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = EncryptPassword(password)
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> IsUserValidAsync(string username, string password)
        {
            var user = await this.context.Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("No such user!");
            }

            if (user.Password == EncryptPassword(password))
            {
                return true;
            }

            return false;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.context.Users
                .Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.context.Users
                .Any(x => x.Username == username);
        }

        private static string EncryptPassword(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);

                foreach (var hashedByte in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(hashedByte.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
