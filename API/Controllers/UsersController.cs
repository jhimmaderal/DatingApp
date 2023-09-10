using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

  [Authorize]
  public class UsersController : BaseApiController
  {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
      _userRepository = userRepository;
      _mapper = mapper;
    }

    // await and async - sync get and send of data to DB
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() // List of DB Users
    {
      var users = await _userRepository.GetMemberAsync();
      return Ok(users);
    }

    [HttpGet("{username}")] //https:5001/api/users/2

    public async Task<ActionResult<MemberDto>> GetUsers(string username)
    {
      return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut] // UPDATE COMMAND
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //NAMEIDENTIFIER FROM JWT TOKEN SERVICE
      var user = await _userRepository.GetUserByUsernameAsync(username);

      if (user == null) return NotFound();

      _mapper.Map(memberUpdateDto, user);

      if (await _userRepository.SaveAllAsync()) return NoContent(); //RETURN 204

      return BadRequest("Failed to update user ");
    }
  }
}
