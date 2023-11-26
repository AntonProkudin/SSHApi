using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSHApi.Controllers.Auth.Requests;
using SSHApi.Controllers.Auth.Responses;
using SSHApi.Functions.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSHApi.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly IUserFunctions _userFunction;
    public AuthController(IConfiguration configuration, IUserFunctions userFunction)
    {
        _configuration = configuration;
        _userFunction = userFunction;
    }

    [Route("Authenticate")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> GetToken([FromBody] LoginUser user)
    {
        var verify = await _userFunction.Verify(user.Email, user.Password);
        if (verify == null)
            return BadRequest();
        AuthenticateResponse response = new AuthenticateResponse();
        response.Email = user.Email;
        response.Password = user.Password;

        if (verify != null)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", response.Email)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);
            response.UserMessage = "Login Success";
            response.UserToken = new JwtSecurityTokenHandler().WriteToken(token);
            response.Id = verify.Id;
            response.LastName = verify.LastName;
            response.FirstName = verify.FirstName;
            response.Password = "";
        }
        else
        {
            response.UserMessage = "Login Failed";
        }
        return Ok(response);
    }
}