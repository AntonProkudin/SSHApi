namespace SSHApi.Functions.User;

public interface IUserFunctions
{
    Task<User?> Verify(string email, string password);

}
