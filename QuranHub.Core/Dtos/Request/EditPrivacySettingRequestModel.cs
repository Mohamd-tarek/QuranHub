using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranHub.Core.Dtos.Request
{
        public class EditPrivacySettingRequestModel : Request
        {
            public bool AllowFollow { get; set; }
            public bool AllowComment { get; set; }
            public bool AllowShare { get; set; }
            public bool AppearInSearch { get; set; }

        }
    
}
