using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAPArchive.Shared.Domain
{
	public class ChatSession
	{
		public Guid Id { get; set; }
		public Guid PredatorId { get; set; }
		public Guid DecoyId { get; set; }
		public string? Name { get; set; }
		public int Rating { get; set; }
		public int ChatLength { get; set; }
        public List<ChatLine>? ChatLines { get; set; }
    }
}
