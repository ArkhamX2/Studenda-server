using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Server.Extension;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Studenda.Core.Model.Account;
using Studenda.Core.Server.Utils.Token;
using Studenda.Core.Server.Utils;
using Studenda.Core.Server.Data;

namespace Studenda.Core.Server.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ServerDataContext _serverDataContext;

        public AccountController(ITokenService tokenService, DataContext context, UserManager<Account> userManager, IConfiguration configuration, ServerDataContext serverDataContext)
        {
            _tokenService = tokenService;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _serverDataContext = serverDataContext;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            var Mapcfg = new MapperConfiguration(cfg => cfg.CreateMap<User, Account>());
            var mapper = new Mapper(Mapcfg);

            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = mapper.Map<Account>(_context.Users.FirstOrDefault(u => u.Email == request.Email));

            if (user is null)
                return Unauthorized();

            var roleIds = await _serverDataContext.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var roles = _serverDataContext.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

            var accessToken = _tokenService.CreateToken(user, roles);
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

            await _context.SaveChangesAsync();

            return Ok(new LoginResponse
            {
                Username = user.Name!,
                Email = user.Email!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);
            var Mapcfg = new MapperConfiguration(cfg => cfg.CreateMap<User, Account>());
            var mapper = new Mapper(Mapcfg);
            var user = new Account
            {
                Name = request.FirstName,
                Surname = request.LastName,
                Patronymic = request.MiddleName,
                Email = request.Email,
                UserName = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if (!result.Succeeded) return BadRequest(request);

            var findUser = mapper.Map<Account>(await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email));

            if (findUser == null) throw new Exception($"User {request.Email} not found");
            List<Role> allRoles = _context.AppRoles.ToList();
            var role = allRoles.FirstOrDefault(x => x.Name == "");

            await _userManager.AddToRoleAsync(findUser, role.Name);

            return await Authenticate(new LoginRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;
            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var username = principal.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }
        [HttpGet]
        [Route("hello")]
        public async Task<List<Model.Common.Department>> Test()
        {
            var u = _context.Departments.ToList();
            return u;
            
        }
    }
}
