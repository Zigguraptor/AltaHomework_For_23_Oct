using System.ComponentModel.DataAnnotations;
using AltaHomework_For_23_Oct.DAL;
using AltaHomework_For_23_Oct.DAL.Entities;
using AltaHomework_For_23_Oct.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AltaHomework_For_23_Oct.Controllers;

public class UsersManagementController : BaseController
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersManagementController(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] Guid? guid)
    {
        var userEntities = guid == null
            ? await _unitOfWork.UsersRepository.GetAsync()
            : await _unitOfWork.UsersRepository.GetAsync(e => e.Guid == guid.Value);

        return Ok(userEntities.Select(u => _mapper.Map<UserVm>(u)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var um = userDto.UserName.ToLower();
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(u => u.UserName.ToLower() == um) != null)
            return BadRequest("UserName already exists.");

        var userEntity = _mapper.Map<UserEntity>(userDto);
        userEntity.Guid = Guid.NewGuid();
        userEntity.CreationDateTime = DateTime.UtcNow;
        userEntity.LastModDateTime = userEntity.CreationDateTime;

        _unitOfWork.UsersRepository.Create(userEntity);

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var userEntity = _unitOfWork.UsersRepository.GetFirstOfDefault(u => u.Guid == dto.Guid);
        if (userEntity == null)
            return NotFound();

        _mapper.Map(dto, userEntity);
        userEntity.LastModDateTime = DateTime.UtcNow;

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var userEntity = _unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == dto.Guid);
        if (userEntity == null)
            return NotFound();

        userEntity.IsActive = false;

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetFriendshipAsync([FromQuery] [Required] string username)
    {
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.UserName == username) == null)
            return BadRequest("user not found");

        var friends = await _unitOfWork.FriendshipRepository.GetFriendsByNameAsync(username);
        return Ok(friends);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFriendshipRequestAsync([FromBody] FriendshipDto friendshipDto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (friendshipDto.ObjectUserGuid == friendshipDto.SubjectUserGuid)
            return BadRequest("Указан один и тот же пользователь");
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == friendshipDto.ObjectUserGuid) == null)
            return BadRequest($"{friendshipDto.ObjectUserGuid} не существует");
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == friendshipDto.SubjectUserGuid) == null)
            return BadRequest($"{friendshipDto.SubjectUserGuid} не существует");

        var errors = _unitOfWork.FriendshipRepository.FriendsRequestAsync(friendshipDto);
        if (errors != null)
            return BadRequest(errors);

        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
