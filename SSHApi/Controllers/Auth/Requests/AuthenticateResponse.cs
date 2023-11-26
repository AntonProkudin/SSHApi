namespace SSHApi.Controllers.Auth.Requests;

public class AuthenticateResponse
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? UserMessage { get; set; }
    public string? UserToken { get; set; }
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
