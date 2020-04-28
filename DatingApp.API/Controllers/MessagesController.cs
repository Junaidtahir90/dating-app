using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helper;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]

    public class MessagesController : ControllerBase
    {
        private readonly IDatingRepositry _repo;
        private readonly IMapper _mapper;
        public MessagesController(IDatingRepositry repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int id)
        {

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messageFromRepo = await _repo.GetMessage(id);

            if (messageFromRepo == null)
            {
                return NotFound();
            }
            var messageToReturn = _mapper.Map<MessageDTO>(messageFromRepo);

            return Ok(messageToReturn);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _repo.GetMessageThread(userId, recipientId);

            var messagesThread = _mapper.Map<IEnumerable<MessageDetailDTO>>(messagesFromRepo);

            return Ok(messagesThread);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(int userId, [FromQuery]MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageParams.UserId = userId;
            var messagesFromRepo = await _repo.GetMessages(messageParams);

            var messages = _mapper.Map<IEnumerable<MessageDetailDTO>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageDTO messageDTO)
        {

            var sender = await _repo.GetUser(userId);

            if (sender == null)
            {
                return BadRequest("Can not find the user");
            }

            if (sender.id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageDTO.senderId = userId;

            var recipient = await _repo.GetUser(messageDTO.recipientId);

            if (recipient == null)
            {
                return BadRequest("Can not find the user");
            }

            var _message = _mapper.Map<Message>(messageDTO);

            _repo.Add(_message);

            if (await _repo.SaveAll())
            {
                var _messageToReturn = _mapper.Map<MessageDetailDTO>(_message);
                return CreatedAtRoute("GetMessage", new
                {
                    id = _message.id
                },
                        _messageToReturn
                );
            }

            return BadRequest("Unable to create message");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messageFromRepo = await _repo.GetMessage(id);

            if (messageFromRepo.recipientId == userId)
            {
                messageFromRepo.recipientDeleted = true;
            }


            if (messageFromRepo.senderId == userId)
            {
                messageFromRepo.senderDeleted = true;
            }

            if (messageFromRepo.recipientDeleted && messageFromRepo.senderDeleted)
            {

                _repo.Delete(messageFromRepo);
            }

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Error in deleting message");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkAsReadMessage(int userId, int id)
        {

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messageFromRepo = await _repo.GetMessage(id);

            messageFromRepo.isRead = true;
            messageFromRepo.dateRead = DateTime.Now;

            await _repo.SaveAll();
            
            return NoContent();

           /* if (await _repo.SaveAll())
            {
                return NoContent();

            }*/

           // return BadRequest("Error in reading message");

        }

    }
}