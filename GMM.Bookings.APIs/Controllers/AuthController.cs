using GMM.Bookings.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GMM.Bookings.APIs.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IConfiguration config;

    public AuthController(IConfiguration config)
    {
      this.config = config;
    }

    private (bool success, string roleName) ValidateUser(string username, string password)
    {
      return (success: true, roleName: password);
    }

    [HttpPost]
    public ActionResult SignIn(UserSignIn item)
    {
      var result = ValidateUser(item.Username, item.Password);
      if (result.success)
      {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var key = Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, item.Username), 
                new Claim(JwtRegisteredClaimNames.Name, item.Username),
                new Claim("role", result.roleName)
             }
          ),
          Expires = DateTime.UtcNow.AddDays(30),
          Issuer = issuer,
          Audience = audience,            
          SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        var jwtResult = new
        {
          Token = stringToken
        };
        return Ok(jwtResult);
      }

      return Unauthorized();
    }


  }
} 
