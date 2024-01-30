using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.Consumers
{
    public interface IMessageConsumer
    {
        int ConsumeMessage();
    }
}
