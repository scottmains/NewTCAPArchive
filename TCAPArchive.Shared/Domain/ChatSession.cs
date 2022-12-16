using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAPArchive.Shared.Domain
{
	public class ChatSession
	{
		public int Id { get; set; }
		public int PredatorId { get; set; }
		public int DecoyId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public Predator Predator { get; set; }
		public Decoy Decoy { get; set; }
		public List<ChatLine> Lines { get; set; }
	}
}
