using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCAPArchive.Shared.Domain
{
    public class Predator
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName{ get; set; }
        public string? Description { get; set; }
        public string? StingLocation { get; set; }
        public string Handle { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        [ValidateNever]
        public List<ChatSession> ChatSessions { get; set; }

    }
}
