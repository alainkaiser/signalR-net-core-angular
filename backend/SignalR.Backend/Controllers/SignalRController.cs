using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SignalR.Backend.Api.Dtos;
using SignalR.Backend.Api.Hubs;
using SignalR.Backend.Api.Settings;

namespace SignalR.Backend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalRController : ControllerBase
    {
        private readonly IHubContext<LiveDataHub> _liveDataHubContext;
        private readonly LiveDataSettings _liveDataSettings;

        public SignalRController(IHubContext<LiveDataHub> liveDataHubContext,
            IOptions<LiveDataSettings> liveDataSettingsOptions)
        {
            _liveDataHubContext = liveDataHubContext;
            _liveDataSettings = liveDataSettingsOptions.Value;
        }

        [HttpPost("{numberOfMessages}")]
        public async Task<ActionResult> PostLiveDataToApp(int numberOfMessages, LiveDataDto liveDataDto)
        {
            for (var i = 0; i < numberOfMessages; i++)
            {
                await _liveDataHubContext.Clients.All.SendAsync(_liveDataSettings.Subject, liveDataDto);    
            }
            
            return NoContent();
        }
    }
}