using Microsoft.EntityFrameworkCore;
using SSHApi.Entities;

namespace SSHApi.Functions.User;

public class UserFunctions : IUserFunctions
{
    private readonly ApiContext _apiContext;

    public UserFunctions(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }
    public async Task<User?> Verify(string email, string password)
    {
        var entity = await _apiContext.TBLUser
           .Where(x => x.Email == email)
           .FirstOrDefaultAsync();
        if (entity == null)
            return null;
        var result = new User
        {
            Id = entity.Id,
            Email = entity.Email,
            Password = entity.Password,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
        };

        if (email == entity.Email && password == entity.Password)
            return result;
        return null;
    }
}
