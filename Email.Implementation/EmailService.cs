using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces.Integrations;

namespace Infrastructure.Implementation
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string address, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
