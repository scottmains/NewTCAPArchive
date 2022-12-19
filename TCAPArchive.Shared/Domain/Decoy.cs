using System.ComponentModel;

namespace TCAPArchive.Shared.Domain
{
    public class Decoy
    {
        public Guid Id { get; set; }
        public string Handle { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
