using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
