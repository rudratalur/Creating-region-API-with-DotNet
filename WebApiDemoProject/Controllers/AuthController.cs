using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemoProject.Models.DTO;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            //validate incoming request

            //Check if user is Authenticated

            //Check trhe userName and password
            var isAuthenticatedUser = await _userRepository.AuthenticateUserAsync(loginRequest.UserName, loginRequest.Password);
            if(isAuthenticatedUser != null)
            {
                //generate JWT
              var token = await  _tokenHandler.CreateTokenAsync(isAuthenticatedUser);
                return Ok(token);
            }
            return BadRequest("username or Password is Incorrect");
            
        }
    }
}
