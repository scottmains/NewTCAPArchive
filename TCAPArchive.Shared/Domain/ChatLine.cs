namespace TCAPArchive.Shared.Domain
{
    public class ChatLine
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
		public Guid SenderId { get; set; }
		public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Position { get; set; }
        public int LikeCount { get; set; }
		public ChatSession Chat { get; set; }

	}
}
