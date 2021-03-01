using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR.Backend.Api.Dtos;

namespace SignalR.Backend.Api.Hubs
{
    public class LiveDataHub: Hub
    {
        public async Task PublishLiveData(string subject, LiveDataDto liveDataDto)
        {
            await Clients.All.SendAsync(subject, liveDataDto);
        }
    }
}