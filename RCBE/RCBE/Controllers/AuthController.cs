using RCBE.DTO;
using RCBE.Helper;
using RCBE.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RCBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<string>> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                var combinedErrors = string.Join("; ", errorMessages);
                return new ApiResponse<string>(null, null, "400", combinedErrors, false, 0, 0, 0, 0, null, null);
            }

            var userByEmail = await _userService.GetUserByEmailAsync(loginModel.Email);
            if (userByEmail == null)
            {
                return new ApiResponse<string>(null, null, "400", "Account is not exists", false, 0, 0, 0, 0, null, null);
            }
            if (userByEmail.IsActived == false)
            {
                return new ApiResponse<string>(null, null, "400", "Inactive Account", false, 0, 0, 0, 0, null, null);
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginModel.Password, userByEmail.Password);
            if (!isPasswordValid)
            {
                return new ApiResponse<string>(null, null, "400", "InCorrect Information", false, 0, 0, 0, 0, null, null);
            }
            var userDTO = await _userService.GetUserDTOAsync(userByEmail.Id);
            if (userDTO == null)
            {
                return new ApiResponse<string>(null, null, "400", "Failed to load Information", false, 0, 0, 0, 0, null, null);
            }
            loginModel.Roles = userDTO.RoleList;

            var token = GenerateToken(userDTO.Id.ToString(), loginModel.Email, loginModel.Roles);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            var roleJson = JsonSerializer.Serialize(userDTO.RoleList);
            Response.Cookies.Append("accessToken", token, cookieOptions);
            Response.Cookies.Append("roles", roleJson, new CookieOptions
            {
                HttpOnly = false, // middleware Edge runtime có thể đọc
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return new ApiResponse<string>(null, token, "200", "Login successfully", true, 0, 0, 0, 0, null, null);

        }

        [Authorize]
        [HttpPost("logout")]
        public ApiResponse<string> Logout()
        {
            Response.Cookies.Delete("accessToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
            Response.Cookies.Delete("roles", new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
            return new ApiResponse<string>(null, null, "200", "Logout successfully", true, 0, 0, 0, 0, null, null);
        }


        private string GenerateToken(string id, string email, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email , email)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            ;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

}
