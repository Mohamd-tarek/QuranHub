using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranHub.Domain.Models
{
    public class Hadith
    {
        public int HadithId { get; set; }

        public int HadithNumber { get; set; }
     
        public string Text { get; set; }

        public int HadithNumberInSection { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }
    }
}
