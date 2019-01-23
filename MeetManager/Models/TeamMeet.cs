using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class TeamMeet
    {
        public int TeamId { get; set;}
        public Team Team { get; set; }

        public int MeetId { get; set; }
        public Meet Meet { get; set; }
    }
}
