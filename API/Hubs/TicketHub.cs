using Microsoft.AspNetCore.SignalR;
using Entity;
namespace API.Hubs
{
    public class TicketHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task NewTicket(EticketViews ticket, EUser creator)
        {
            await Clients.All.SendAsync("newTicketAdded", ticket, creator);
        }
        public async Task ChangeStatus(EticketAndStatus ticketAndStatus, EUser creator)
        {
            await Clients.All.SendAsync("statusChanged", ticketAndStatus, creator);
        }
        public async Task AssginTicketUser(EticketAndUser ticketAndUser,EUser assigneduser,EUser creator)
        {
            await Clients.All.SendAsync("assginTicketUser", ticketAndUser, assigneduser, creator);
        }
        public async Task CreateTicketService(EticketAndService ticketAndService, EUser creator)
        {
            await Clients.All.SendAsync("createTicketService", ticketAndService, creator);
        }

    }
}
