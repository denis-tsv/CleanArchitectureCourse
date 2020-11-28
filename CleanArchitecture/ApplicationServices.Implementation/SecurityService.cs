using System;
using ApplicationServices.Interfaces;

namespace ApplicationServices.Implementation
{
    public class SecurityService : ISecurityService
    {
        public bool IsCurrentUserAdmin { get; }
        public string[] CurrentUserPermissions { get; }
    }
}
