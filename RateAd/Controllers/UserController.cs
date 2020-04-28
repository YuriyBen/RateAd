using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer3.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateAd.DTO;
using RateAd.Models;
using RateAd.Services;
//  connection string   ----  "Data Source=localhost;Initial Catalog=RateAd;Integrated Security=True"
//to update you DB please write this command in Package Manager Console:
//Scaffold-DbContext -Connection Name=RateAd Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
namespace RateAd.Controllers 
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRateAdRepository _rateAdRepository;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public UserController(IRateAdRepository rateAdRepository, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _rateAdRepository = rateAdRepository;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }





        [HttpGet("{userId}", Name ="GetUser")]
        public ActionResult<UserDTO> GetUser(long userId)
        {
            var user = _rateAdRepository.GetUser(userId);
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser(UserForCreationDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            _rateAdRepository.CreateUser(userEntity);
            _rateAdRepository.Save();

            var userToReturn = _mapper.Map<UserDTO>(userEntity);
            return CreatedAtAction("GetUser",
                        new { userId = userEntity.Id },
                        userToReturn);
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _rateAdRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }




        [HttpGet("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            //user = await _context.Users
            //                            .Where(u => u.UserName == user.UserName
            //                                    && u.PasswordHash == user.PasswordHash)
            //                            .FirstOrDefaultAsync();


            user.PasswordHash = null;
            user.PasswordSalt = null;


            UserWithToken userWithToken = new UserWithToken(user);

            if (userWithToken == null)
            {
                return NotFound();
            }

            // sign my token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires=DateTime.UtcNow.AddMonths(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token=tokenHandler.WriteToken(token);


            return userWithToken;
        }


    }
}