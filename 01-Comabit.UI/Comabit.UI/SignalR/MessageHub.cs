using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comabit.UI.Models.Message;
using Microsoft.AspNetCore.SignalR;

namespace Comabit.UI.SignalR
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendMatchMessage(Guid companyId, Guid matchId, Guid messageId)
        {
            await Clients.User(companyId.ToString()).ReceiveMatchMessage(matchId, messageId);
        }

        public async Task ReceiveNotificationUpdate(Guid companyId, int totalNewMessages)
        {
            await Clients.User(companyId.ToString()).ReceiveNotificationUpdate(totalNewMessages);
        }

        public async Task BroadcastMessage(string name, string message)
        {
            await Clients.All.ReceiveMessage(name, message);
        }

        public async Task SendMessage(string companyId, string name, string message)
        {
            await Clients.User(companyId).ReceiveMessage(name, message);
            await Clients.User(companyId).ReceiveNotificationUpdate(20); // TODO
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
