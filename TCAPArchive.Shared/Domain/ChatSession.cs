
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TCAPArchive.Shared.Domain
{
	public class ChatSession
	{
		public Guid Id { get; set; }

		public Guid PredatorId { get; set; }
        public Predator? Predator { get; set; }

        public Guid DecoyId { get; set; }
        public Decoy? Decoy { get; set; }

        public string? Name { get; set; }
		public int Rating { get; set; }
		public int ChatLength { get; set; }

        public List<ChatLine>? ChatLines { get; set; }
    }
}
