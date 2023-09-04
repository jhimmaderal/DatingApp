using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

  [Authorize]
  public class UsersController : BaseApiController
  {
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
      _context = context;
    }

    // await and asyncg - sync get and send of data to DB
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // List of DB Users
    {
      var users = await _context.Users.ToListAsync();

      return users;
    }


    [HttpGet("{id}")] //https:5001/api/users/2
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
      return await _context.Users.FindAsync(id);
    }
  }
}
