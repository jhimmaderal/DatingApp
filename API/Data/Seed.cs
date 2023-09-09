using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUers(DataContext context) //Parse DataContext
        {
            if (await context.Users.AnyAsync()) return; // Check Exisiting Users

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json"); // Read All in Json File

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true}; //Case Insensitive

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); //Json to C# Object

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}