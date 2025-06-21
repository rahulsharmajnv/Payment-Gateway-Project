using EcommerceBackend.Api.Data;
using EcommerceBackend.Api.Models;
using Microsoft.EntityFrameworkCore; // Add this line or update with the correct namespace for AppDbContext
using System.IdentityModel.Tokens.Jwt; // Required for JwtSecurityTokenHandler
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace EcommerceBackend.Api.Services
{
    public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
            return (false, "User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return (true, "Registered");
    }

    public async Task<(bool Success,string Token)> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return (false, null);

        var claims = new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return (true,new JwtSecurityTokenHandler().WriteToken(token));
    }
}
}