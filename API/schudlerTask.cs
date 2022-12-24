
using LOGIC;
using Entity;
using DAL.Functions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using API.Hubs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using DAL.Interfaces;

namespace API
{

    public class TimedHostedService : IHostedService, IDisposable
    {

        private readonly IHubContext<TicketHub> _hubContext;
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;


        public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<TicketHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
             TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);


            List<EticketViews> lOldTicket = new List<EticketViews>();

            Dtickets eoDticket = new Dtickets();
            lOldTicket = eoDticket.getallServicebefore3datys();

            if(lOldTicket.Count > 0)
            {

                // update status for all services

                
                foreach (EticketViews ticket in lOldTicket)
                {

                    EticketAndStatus ticketAndStatus = new EticketAndStatus();
                     ticketAndStatus.ticketStatusId = 5;
                    ticketAndStatus.ticketId=ticket.ticketId;
                    ticketAndStatus.UserId = 0;
                   await eoDticket.insertNewStatus(ticketAndStatus);
                    // call updatestatus API
                    EUser oeEuser = new EUser();
                    oeEuser.UserId = 0;
                    oeEuser.FirstName = "System";
                    oeEuser.Lastname = "";
                    oeEuser.Email = "";
                    oeEuser.AuthLevelRefId = 1;
                    //APITickets Apitick = new APITickets(_hubContext);
                    //Apitick.SendNotification(ticketAndStatus);

                    await _hubContext.Clients.All.SendAsync("statusChanged", ticketAndStatus, oeEuser);
                }
            }

            var ticketCount = lOldTicket.Count;
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count} number of tickets={ticketCount}", count, ticketCount);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
