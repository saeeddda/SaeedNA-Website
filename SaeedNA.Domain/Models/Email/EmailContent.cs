namespace SaeedNA.Domain.Models.Email
{
    public class EmailContent
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; } = false;
    }
}
