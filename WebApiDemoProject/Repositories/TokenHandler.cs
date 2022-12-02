using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public TokenHandler(IConfiguration configuration)
        //{
        //   configuration _configuration
        //}
        public Task<string> CreateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.GivenName, user.FistName));
            cliams.Add(new Claim(ClaimTypes.Surname, user.LastName));
            cliams.Add(new Claim(ClaimTypes.Email, user.EmilAddress));

            //loop using forech

            user.Roles.ForEach(role =>
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            });

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                cliams,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: credentials
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
