namespace TCAPArchive.Shared.Domain
{
    public class ChatLine
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public int Position { get; set; }
        public int LikeCount { get; set; }
        public Guid? PredatorId { get; set; }
        public Predator predator { get; set; }

        public Guid? DecoyId { get; set; }
        public Decoy Decoy { get; set; }
      

    }
}
