namespace EmailApp.Entities
{
    public class Message
    {
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
        public string MessageType { get; set; }
        public bool isRead { get; set; }
        public bool situation { get; set; }
        public string MessageCategory { get; set; }
    }
}
