using SuggestionPanel.Domain.DTOs;

namespace SuggestionPanel.Application.Services.SMTP
{
    public interface ISMTPService
    {
        bool SendEmail(MailRequestDto request);
    }
}
