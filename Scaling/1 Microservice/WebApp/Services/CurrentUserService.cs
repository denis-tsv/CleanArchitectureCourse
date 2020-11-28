using Infrastructure.Interfaces;

namespace WebApp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email => "test@test.test";
    }
}
