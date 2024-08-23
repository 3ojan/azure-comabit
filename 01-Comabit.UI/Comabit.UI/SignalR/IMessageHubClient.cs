using Comabit.UI.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.SignalR
{
    public interface IMessageHubClient
    {
        Task ReceiveMatchMessage(Guid matchId, Guid messageId);

        Task ReceiveNotificationUpdate(int totalNewMessages);

        Task BroadcastMessage(string name, string message);

        Task ReceiveMessage(string name, string message);
    }
}
