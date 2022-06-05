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
    public class NoteController : Controller
    {
        private DataContext context;
        private ILogger<HomeController> logger;

        public NoteController(ILogger<HomeController> _logger, DataContext ctx)
        {
            logger = _logger;  
            context = ctx;
        }

        

        [HttpGet("{id}")]
        public Note GetNote(long id)
        {
         
            return context.Note.FirstOrDefault(d=> d.Index == id);
        }

        
        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note) {
            logger.LogDebug(note.Index,ToString());
            if (ModelState.IsValid) {
               if(context.Note.Any((d=> d.Index == note.Index)))
               {
                   Note cur = context.Note.Where((d=> d.Index == note.Index)).First();
                   cur.Text = note.Text;
               }
               else{
                    context.Add(note);
               }
                
                context.SaveChanges();
                return Ok(note.Id);
            } else {
                return BadRequest(ModelState);
            }
        }

    }
}
