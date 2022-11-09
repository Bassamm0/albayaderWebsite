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
        public async Task NewTicket(EticketViews ticket)
        {
            await Clients.All.SendAsync("newTicketAdded", ticket);
        }
        public async Task ChangeStatus(int ticketId,int ticketStatusId,string StatusName)
        {
            await Clients.All.SendAsync("changeStatus", ticketId, ticketStatusId, StatusName);
        }
        public async Task AssginTicketUser(int ticketId, string UserName)
        {
            await Clients.All.SendAsync("assginTicketUser", ticketId, UserName);
        }
        public async Task CreateTicketService(int ticketId, int serviceId)
        {
            await Clients.All.SendAsync("createTicketService", ticketId, serviceId);
        }
    }
}
