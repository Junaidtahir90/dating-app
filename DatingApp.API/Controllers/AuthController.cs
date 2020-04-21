using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using System.Web.Http;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // to add vaidation

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepositry _authRepositry;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepositry authRepositry, IConfiguration config, IMapper mapper)
        {
            _authRepositry = authRepositry;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO _userDTO)
        {

            // Validate user Request

            // if (!ModelState.IsValid) and [FromBody] we do not need if we use are using [ApiController]
            //    return BadRequest(ModelState);

            _userDTO.Username = _userDTO.Username.ToLower();

            if (await _authRepositry.UserExits(_userDTO.Username))
            {
                return BadRequest("Username Already Exists");
                /* return BadRequest (new{
                errors="Username Already Exists"
                } );*/
            }

            /*var newUserCreation=new User {
                username=_userDTO.Username
            };*/

            var newUserCreation = _mapper.Map<User>(_userDTO);

            var newUser = await _authRepositry.Register(newUserCreation, _userDTO.Password);
            var userDTO = _mapper.Map<UserDetailDTO>(newUserCreation);

            return CreatedAtRoute
            ("GetUser", new
            {
                Controller = "Users",
                id = userDTO.id,
            },
            userDTO
            );
            //return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO _loginDTO)
        {

            // throw new Exception ("Server Down,Maintainance Mode,Get back Soon...!!!");
            var existingUser = await _authRepositry.Login(_loginDTO.Username, _loginDTO.Password);

            if (existingUser == null)
                return Unauthorized();

            var claims = new[]{
                   new Claim (ClaimTypes.NameIdentifier,existingUser.id.ToString()),
                   new Claim (ClaimTypes.NameIdentifier,existingUser.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            // 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = _mapper.Map<UserListDTO>(existingUser);//,user)

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = user
            }
            );
        }
    }
}