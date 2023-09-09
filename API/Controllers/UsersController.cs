using API.DTOs;
using API.Entities;
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
  }
}
