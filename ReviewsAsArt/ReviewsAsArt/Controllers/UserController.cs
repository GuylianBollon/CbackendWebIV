using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReviewsAsArt.DTO;
using ReviewsAsArt.Models;

namespace ReviewsAsArt.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _iur;
        private readonly IConfiguration _config;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository iur, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _iur = iur;
            _config = config;
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _iur.GetUsers();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("zoek/{berichts}")]
        public IEnumerable<Bericht> GetBerichts()
        {
            User user = _iur.GetBy(User.Identity.Name);
            return user.Berichten;
        }
        [AllowAnonymous]
        [HttpGet("reviews/{user}")]
        public IEnumerable<Review> GetReviewsVanUser(User user)
        {
            return _iur.ToonReviewsVanUser(user);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("feed/{users}")]
        public IEnumerable<Review> GetReviewsVanSubscripties(IEnumerable<User> user)
        {
            return _iur.ToonReviewsVanSubscripties(user);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("addreview/{review}")]
        public void AddReviewTotUser(Review review)
        {
            User user = _iur.GetBy(User.Identity.Name);
            user.Reviews.Add(review);
            _iur.SaveChanges();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{user}/subscribe")]
        public void SubscribeUser(User user)
        {
            User user2 = _iur.GetBy(User.Identity.Name);
            user2.Users.Add(user);
            _iur.SaveChanges();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{user}/unsubscribe")]
        public void UnsubscribeUser(User user)
        {
            User user2 = _iur.GetBy(User.Identity.Name);
            user2.Users.Remove(user);
            _iur.SaveChanges();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token);
                }
            }
            return BadRequest();
        }

        private string GetToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            User customer = new User() { Email = model.Email, UserName = model.Username};
            var result = await _userManager.CreateAsync(customer, model.Password);
            if (result.Succeeded)
            {
                string token = GetToken(customer);
                return Created("", token);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null;
        }
    }
}