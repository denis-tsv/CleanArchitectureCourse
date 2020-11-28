using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces.WebApp;

namespace WebApp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email => "test@test.test";
    }
}
