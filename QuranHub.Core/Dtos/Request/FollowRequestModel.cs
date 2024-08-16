using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranHub.Core.Dtos.Request
{
    public class FollowRequestModel
    {
        public string FollowerId { get; set; }
        public string FollowedId { get; set; }
    }
}
