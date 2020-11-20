
using System.Collections.Generic;

namespace Domain.Crosscutting.MessageBroker
{
    public interface IMessageBroker<T> where T : BaseEntity
    {

        void Send(T item);

        void Send(List<T> items);
    }
}
