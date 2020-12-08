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
            Console.WriteLine($"Email to {address} subject '{subject}' body '{body}'");
            Console.Out.Flush();

            return Task.CompletedTask;
        }
    }
}
