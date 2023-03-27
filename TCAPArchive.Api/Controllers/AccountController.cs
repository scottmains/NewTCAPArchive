using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (result)
        {
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized();
        }
    }
    private string GenerateJwtToken(ApplicationUser user)
    {
        var header = new JwtHeader(new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
            );

        var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
};

        var expireDays = Convert.ToDouble(_configuration["Jwt:ExpireDays"] ?? "7");
        var now = DateTime.UtcNow;
        var unixTimestampNow = new DateTimeOffset(now).ToUnixTimeSeconds();
        var unixTimestampExp = new DateTimeOffset(now.AddDays(expireDays)).ToUnixTimeSeconds();

        var payload = new JwtPayload
{
    {"iss", _configuration["Jwt:Issuer"]},
    {"aud", _configuration["Jwt:Audience"]},
    {"exp", unixTimestampExp},
    {"iat", unixTimestampNow},
    {"nbf", unixTimestampNow},
    {"unique_name", user.UserName}
};

        payload.AddClaims(claims);

        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    [Authorize]
    [HttpGet("getcurrentuser")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return BadRequest("No user claims found.");
        }
        return Ok(user);
    }
}