using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.WebApp
{
    public interface ICurrentUserService
    {
        string Email { get; }
    }
}
