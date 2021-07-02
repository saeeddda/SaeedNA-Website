namespace SaeedNA.Domain.Models.Email
{
    public class EmailProvider
    {
        public string HostName { get; set; }
        public string HostPort { get; set; }
        public string HostUsername { get; set; }
        public string HostPassword { get; set; }
        public string Sender { get; set; }
    }
}
