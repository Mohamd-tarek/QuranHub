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
    public class DataController : Controller
    {
        private DataContext context;

        public DataController(ILogger<HomeController> logger, DataContext ctx)
        {
           
            context = ctx;
        }

        [Route("Quran")]
        [HttpGet]
        public IEnumerable<Quran> GetQuran()
        {
            return context.Quran.OrderBy(d=> d.Index);
        }

        [Route("Tafseer")]
        [HttpGet]
        public IEnumerable<Tafseer> GetTafseer()
        {
            return context.Tafseer.OrderBy(d => d.Index);
        }

        [Route("Translation")]
        [HttpGet]
        public IEnumerable<Translation> GetTranslation()
        {
            return context.Translation.OrderBy(d => d.Index);
        }

        [Route("QuranClean")]
        [HttpGet]
        public IEnumerable<QuranClean> GetQuranClean()
        {
            return context.QuranClean.OrderBy(d=> d.Index);
        }

        [Route("Note")]
        [HttpGet("{index}")]
        public Note GetNote(int index)
        {
            return context.Note.Where(d=> d.Index == index).First();
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Note note) {
            if (ModelState.IsValid) {
                context.Add(note);
                context.SaveChanges();
                return Ok(note.Id);
            } else {
                return BadRequest(ModelState);
            }
        }

        // meta data
        [Route("Hizbs")]
        [HttpGet]
        public IEnumerable<Hizb> GetHizbs()
        {
            return context.Hizbs.OrderBy(d => d.Index);
        }

        [Route("Juzs")]
        [HttpGet]
        public IEnumerable<Juz> GetJuzs()
        {
            return context.Juzs.OrderBy(d => d.Index);
        }

        [Route("Manzils")]
        [HttpGet]
        public IEnumerable<Manzil> GetManzils()
        {
            return context.Manzils.OrderBy(d => d.Index);
        }

        [Route("Pages")]
        [HttpGet]
        public IEnumerable<Page> GetPages()
        {
            return context.Pages.OrderBy(d => d.Index);
        }

        [Route("Rukus")]
        [HttpGet]
        public IEnumerable<Ruku> GetHRukus()
        {
            return context.Rukus.OrderBy(d => d.Index);
        }

        [Route("Sajdas")]
        [HttpGet]
        public IEnumerable<Sajda> GetSajdas()
        {
            return context.Sajdas.OrderBy(d => d.Index);
        }

        [Route("Suras")]
        [HttpGet]
        public IEnumerable<Sura> GetSuras()
        {
            return context.Suras.OrderBy(d => d.Index);
        }
    }
}
