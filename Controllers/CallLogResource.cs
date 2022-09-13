using ContractTesting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractTesting.Controllers
{
    [ApiController]
    [Route("calllogs")]
    [Produces("application/json")]
    public class CallLogResource : ControllerBase
    {

        private readonly ILogger<CallLogResource> _logger;
        private readonly CallLogService callLogService;

        public CallLogResource(ILogger<CallLogResource> logger, CallLogService callLogService)
        {
            this._logger = logger;
            this.callLogService = callLogService;
        }

        [HttpGet]
        public IEnumerable<CallLogDto> Get() => callLogService.FindAllCallLogs(_logger);
    }
}
