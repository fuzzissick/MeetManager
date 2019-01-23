using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class Race
    {
        public int Id { get; set; }

        public Event Event{ get; set; }
        public Meet Meet { get; set; }
        public Runner Runner { get; set; }
        public string Time { get; set; }
    }
}
