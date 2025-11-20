
using cu.liveSystem.blazor.Hubs;
using cu.liveSystem.blazor.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace cu.liveSystem.blazor.Services
{
    public class StatusService : BackgroundService
    {
        //reference to hub
        private readonly IHubContext<StatusHub> _hubContext;
        private readonly Uri _uri;
        private readonly HttpClient _httpClient;

        public StatusService(IHubContext<StatusHub> hubContext, HttpClient httpClient)
        {
            _hubContext = hubContext;
            _uri = new Uri("https://api.chucknorris.io/jokes/random");
            _httpClient = httpClient;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var guid = Guid.NewGuid();
                //use hub to broadcast
                await _hubContext.Clients.All.SendAsync("ReceiveStatusUpdate", "system", guid);
                await _hubContext.Clients.All
                    .SendAsync("ReceiveQuote", await GetQuote());
                await Task.Delay(3000);//wait three seconds
            }
        }
        protected async Task<QuoteModel> GetQuote()
        {
            var result = await _httpClient.GetAsync(_uri.AbsoluteUri);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QuoteModel>(content);
        }
    }
}
