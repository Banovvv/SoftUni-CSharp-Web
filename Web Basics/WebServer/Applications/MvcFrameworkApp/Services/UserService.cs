using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

        public async Task<string> CreateUserAsync(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = EncryptPassword(password)
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<string> GetUserIdAsync(string username, string password)
        {
            var user = await this.context.Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            return user?.Id;
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            return !await this.context.Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return !await this.context.Users
                .AnyAsync(x => x.Username == username);
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
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
