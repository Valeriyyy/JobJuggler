namespace JobJuggler.DTO.Identity;

public class UserDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public CompanyDTO Company { get; set; }
    
}