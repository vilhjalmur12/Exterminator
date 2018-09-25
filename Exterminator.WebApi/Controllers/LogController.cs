using Exterminator.Services.Interfaces;
using Exterminator.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
    [Route("api/logs")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController (ILogService serv) {
            _logService = serv;
        }

        // TODO: Implement route which gets all logs from the ILogService, which should be injected through the constructor
        [HttpGet("")]
        public IActionResult GetAllLogs() {
            return Ok(_logService.GetAllLogs());
        }
    }
}