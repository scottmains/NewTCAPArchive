using System.ComponentModel;

namespace TCAPArchive.Shared.Domain
{
    public class Decoy
    {
        public Guid Id { get; set; }
       public Guid ChatId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? StingLocation { get; set; }
        public string Handle { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public List<ChatLine> ChatLines { get; set; }
    
    }
}
