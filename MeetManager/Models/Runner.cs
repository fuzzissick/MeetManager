using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class Runner
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EventGroup { get; set; }
        public string Year { get; set; }
        public Team Team { get; set; }

        public virtual ICollection<Race> Races { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [NotMapped]
        public string FullNameAndCollege
        {
            get
            {
                return string.Format("{0} {1} - {2}", FirstName, LastName, Team.CollegeName);
            }
        }
    }
}
