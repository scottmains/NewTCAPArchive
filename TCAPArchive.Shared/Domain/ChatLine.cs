
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TCAPArchive.Shared.Domain
{
    public class ChatLine
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderHandle { get; set; }
		public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
   
        public int Position { get; set; }
        public int LikeCount { get; set; }

        public Guid ChatSessionId { get; set; }
        public ChatSession? ChatSession { get; set; }

    }
}
