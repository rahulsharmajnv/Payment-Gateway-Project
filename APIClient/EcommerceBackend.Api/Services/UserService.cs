using EcommerceBackend.Api.Data;

namespace EcommerceBackend.Api.Services
{
    public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return new { user.Id, user.Name, user.Email };
    }
}
}