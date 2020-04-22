using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    //[Authorize]
    //[Route("api/controller")]
    [Authorize]
    //http://localhost:5000/api/values/
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepositry _repo;
        public IMapper _mapper { get; }
        public UsersController(IDatingRepositry repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {

            var users = await _repo.GetUsers(userParams);

            var usersDTO=_mapper.Map<IEnumerable<UserListDTO>> (users);
            Response.AddPagination(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages);
            return Ok(usersDTO);
        }

        [HttpGet("{id}",Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = await _repo.GetUser(id);
            var userDTO= _mapper.Map<UserDetailDTO>(user);

            return Ok(userDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,UserDataForUpdateDTO updateDTO){

            if( id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized();
            }

            var userFromRepo=await _repo.GetUser(id);
            _mapper.Map(updateDTO,userFromRepo);

            if(await _repo.SaveAll()){
                return NoContent();
            }

            throw new Exception($"Updating user{id} failed on saved");
        }


    }
}