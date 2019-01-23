using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string CollegeName { get; set; }
        public string Mascot { get; set; }
        public string Location { get; set; }
        public string Coach { get; set; }
        public string Conference { get; set; }

        public virtual ICollection<TeamMeet> TeamMeets { get; set; }
        public virtual ICollection<Runner> Runners { get; set; }

    }
}
