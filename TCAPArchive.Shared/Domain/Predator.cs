using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAPArchive.Shared.Domain
{
    public class Predator
    {

        public Guid PredatorId { get; set; } 
        public Guid ChatId { get; set; }
        public Decoy Decoy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Handle { get; set; }
        public DateTime BirthDate { get; set; }
        public string StingLocation { get; set; }
        public byte[] ImageData { get; set;  }
    }
}
