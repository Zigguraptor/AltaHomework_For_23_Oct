using AltaHomework_For_23_Oct.DAL;
using AltaHomework_For_23_Oct.DAL.Entities;
using AltaHomework_For_23_Oct.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AltaHomework_For_23_Oct.Controllers;

public class MessagesController : BaseController
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MessagesController(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOutMessagesAsync([FromQuery] Guid? userGuid)
    {
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == userGuid) == null)
            return NotFound();

        var messages = await _unitOfWork.MessagesRepository.GetAsync(m => m.SenderGuid == userGuid,
            includeProperties: "Sender,Recipient");
        var messagesVm = messages.Select(e => _mapper.Map<MessageVm>(e)).ToList();
        return Ok(messagesVm);
    }

    [HttpGet]
    public async Task<IActionResult> GetInMessagesAsync([FromQuery] Guid? userGuid)
    {
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == userGuid) == null)
            return NotFound();

        var messages = await _unitOfWork.MessagesRepository.GetAsync(m => m.RecipientGuid == userGuid,
            includeProperties: "Sender,Recipient");
        var messagesVm = messages.Select(e => _mapper.Map<MessageVm>(e)).ToList();
        return Ok(messagesVm);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessagesAsync([FromBody] MessageDto messageDto)
    {
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == messageDto.SenderGuid) == null)
            return NotFound("SenderGuid not found");
        if (_unitOfWork.UsersRepository.GetFirstOfDefault(e => e.Guid == messageDto.RecipientGuid) == null)
            return NotFound("RecipientGuid not found");

        var messageEntity = _mapper.Map<MessageEntity>(messageDto);
        messageEntity.CreationDateTime = DateTime.UtcNow;
        messageEntity.LastModDateTime = messageEntity.CreationDateTime;

        _unitOfWork.MessagesRepository.Create(messageEntity);
        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
