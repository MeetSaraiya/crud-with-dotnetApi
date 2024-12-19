using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using crud_with_dotnetApi.Data;
using crud_with_dotnetApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace crud_with_dotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeRepositery _employeeRepository;
        private readonly JwtOptions _options;

        public AuthController(EmployeeRepositery employeeRepository, IOptions<JwtOptions> options)
        {
            _employeeRepository = employeeRepository;
            _options = options.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginDto model)
        {
            var employee = await _employeeRepository.GetEmployeeByName(model.Name);
            if (employee == null)
            {
                return BadRequest(new { error = "Name does not exist" });
            }
            if(employee.Password != model.Password)
            {
                return BadRequest(new { error = "password does not match" });
            }
            var jwtKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key));
            var credentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Name",model.Name)
            };
            var sToken = new JwtSecurityToken(_options.Key, _options.Issuer,expires:DateTime.Now.AddMinutes(30),signingCredentials:credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            return Ok(new {token = token});
        }
    }
}
