using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepositry _authRepositry;
        public AuthController(IAuthRepositry authRepositry)
        {
                _authRepositry=authRepositry;
        }


     [HttpPost("register")]
        public  async Task<IActionResult> Register (UserDTO _userDTO){

            // Validate user Request

             _userDTO.Username =_userDTO.Username.ToLower();

             if(await _authRepositry.UserExits(_userDTO.Username)) {
                 return BadRequest ("OOOPS,Username Already Exists!!!!!");
             }

            var newUserCreation=new User {
                username=_userDTO.Username
            }; 

            var newUser= await _authRepositry.Register(newUserCreation,_userDTO.Password);

            return StatusCode(201);


        }
    }
}