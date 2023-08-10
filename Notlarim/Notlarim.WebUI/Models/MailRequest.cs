namespace Notlarim.WebUI.Models
{
    public class MailRequest
    {
        public int MessagesId { get; set; }
        public string Name { get; set; }
        public string AnswerName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string AnswerTitle { get; set; }
        public string Message { get; set; }
        public string AnswerMessage { get; set; }
        public bool? MessageStatus { get; set; }
    }
}
