using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DatingApp.API.Controllers
{
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
        public async Task<IActionResult> GetUsers()
        {

            var users = await _repo.GetUsers();
            var usersDTO=_mapper.Map<IEnumerable<UserListDTO>> (users);

            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = await _repo.GetUser(id);
            var userDTO= _mapper.Map<UserDetailDTO>(user);

            return Ok(userDTO);
        }

    }
}