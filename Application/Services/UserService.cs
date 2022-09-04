using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class UserService : IUserService
{
    public void GetUser()
    {
        Console.WriteLine("this is where it would get a user");
    }
}
