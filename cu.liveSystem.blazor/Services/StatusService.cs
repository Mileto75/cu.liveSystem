
using cu.liveSystem.blazor.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace cu.liveSystem.blazor.Services
{
    public class StatusService : BackgroundService
    {
        //reference to hub
        private readonly IHubContext<StatusHub> _hubContext;

        public StatusService(IHubContext<StatusHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var guid = Guid.NewGuid();
                //use hub to broadcast
                await _hubContext.Clients.All.SendAsync("ReceiveStatusUpdate", "system", guid);
                await Task.Delay(3000);//wait three seconds
            }
        }
    }
}
