using System;

namespace ApplicationServices.Interfaces
{
    public interface ISecurityService
    {
        bool IsCurrentUserAdmin { get; }
        string[] CurrentUserPermissions { get; }
    }
}
