
namespace API.Entities;

public class AppUser
{
//[Key] for primary Key
 public int id { get; set; }
 public string UserName { get; set; }
 public byte[] PasswordHash { get; set; }
 public byte[] PasswordSalt { get; set; }

}
