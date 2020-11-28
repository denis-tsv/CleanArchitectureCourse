using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Integrations
{
    public interface IEmailService
    {
        Task SendAsync(string address, string subject, string body);
    }
}
