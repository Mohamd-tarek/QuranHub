using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MindMapController : Controller
    {
        private DataContext context;
        private ILogger<HomeController> logger;

        public MindMapController(ILogger<HomeController> _logger, DataContext ctx)
        {
            logger = _logger;  
            context = ctx;
        }

        

        [HttpGet("{id}")]
        public byte[] GetMindMap(long id)
        {
            return context.MindMaps.Where(d=> d.Index == id).Select(m => m.MapImage).FirstOrDefault();
        }


    }
}
