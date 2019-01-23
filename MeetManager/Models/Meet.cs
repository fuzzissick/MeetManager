using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class Meet
    {
        public int Id { get; set; }

        public string Location { get; set; }
        public string Name { get; set; }
        public Team Team { get; set; }

        public virtual ICollection<TeamMeet> TeamMeets { get; set; }
        public virtual ICollection<Race> Races { get; set; }
    }
}
