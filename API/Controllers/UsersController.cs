using API.DTOs;
using API.Entities;
using API.Extensions;
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
    private readonly iPhotoService _photoService;

    public UsersController(IUserRepository userRepository, IMapper mapper, iPhotoService photoService)
    {
      _userRepository = userRepository;
      _mapper = mapper;
      _photoService = photoService;
    }

    // await and async - sync get and send of data to DB
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() // List of DB Users
    {
      var users = await _userRepository.GetMemberAsync();
      return Ok(users);
    }

    [HttpGet("{username}")] //https:5001/api/users/2

    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
      return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut] // UPDATE COMMAND
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
      var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername()); //NAMEIDENTIFIER FROM JWT TOKEN SERVICE

      if (user == null) return NotFound();

      _mapper.Map(memberUpdateDto, user);

      if (await _userRepository.SaveAllAsync()) return NoContent(); //RETURN 204

      return BadRequest("Failed to update user ");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
      var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

      if (user == null) return NotFound();

      var result = await _photoService.AddPhotoAsync(file);

      if (result.Error != null) return BadRequest(result.Error.Message);

      var photo = new Photo
      {
        Url = result.SecureUrl.AbsoluteUri,
        PublicId = result.PublicId
      };

      if (user.Photos.Count == 0) photo.IsMain = true;

      user.Photos.Add(photo);

      if (await _userRepository.SaveAllAsync())
      {
        return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo)); // RETURN 201 THAN 200
      };

      return BadRequest("Problem Adding Photo");
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
      var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

      if (user == null) return NotFound();

      var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

      if (photo == null) return NotFound();

      if (photo.IsMain) return BadRequest("This is already you main photo");

      var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

      if (currentMain != null) currentMain.IsMain = false;

      photo.IsMain = true;

      if (await _userRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Problem Setting the Main Photo");
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
      var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

      var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

      if (photo == null) return NotFound();

      if (photo.IsMain) return BadRequest("You Cannot Delete Main Photo");

      if (photo.PublicId != null)
      {
        var result = await _photoService.DeletePhotoAsync(photo.PublicId);

        if (result.Error != null) return BadRequest(result.Error.Message);

      }

      user.Photos.Remove(photo);

      if (await _userRepository.SaveAllAsync()) return Ok();

      return BadRequest("Problem Deleting Photo");
    }
  }
}
