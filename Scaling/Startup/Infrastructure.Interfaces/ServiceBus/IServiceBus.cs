using System.Threading.Tasks;

namespace Infrastructure.Interfaces.ServiceBus
{
    public interface IServiceBus
    {
        Task SendMessageAsync(Message message);
    }
}
