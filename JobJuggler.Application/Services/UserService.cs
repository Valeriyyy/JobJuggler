using JobJuggler.Application.Services.Interfaces;

namespace JobJuggler.Application.Services;

public class UserService : IUserService {
    public void GetUser() {
        Console.WriteLine("this is where it would get a user");
    }
}
