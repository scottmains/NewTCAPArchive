
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TCAPArchive.Shared.Domain
{
    public class Decoy
    {
        public Guid Id { get; set; }
        public Guid? PredatorId { get; set; }
        public string Handle { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        
        public List<ChatSession>? ChatSessions { get; set; }
    }
}
