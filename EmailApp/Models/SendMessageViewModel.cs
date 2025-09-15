namespace EmailApp.Models
{
    public class SendMessageViewModel
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MessageType { get; set; }
        public string MessageCategory { get; set; }
        public bool isRead { get; set; }
        public bool situation {  get; set; }
    }
}
