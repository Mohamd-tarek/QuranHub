using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranHub.Domain.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public int HadithNumberStart { get; set; }
        public int HadithNumberEnd { get; set; }

        public List<Hadith> Hadiths { get; set; }
    }
}
