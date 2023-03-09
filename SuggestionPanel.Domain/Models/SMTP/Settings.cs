namespace SuggestionPanel.Domain.Models.SMTP
{
    public class Settings
    {
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Host { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
