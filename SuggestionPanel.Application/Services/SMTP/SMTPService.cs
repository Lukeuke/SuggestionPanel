using SuggestionPanel.Domain.DTOs;
using System.Net.Mail;
using System.Net;
using SuggestionPanel.Domain.Models.SMTP;

namespace SuggestionPanel.Application.Services.SMTP
{
    public class SMTPService : ISMTPService
    {
        private readonly Settings _settings;

        public SMTPService(Settings settings)
        {
            _settings = settings;
        }

        public bool SendEmail(MailRequestDto request)
        {
            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.Username),
                To = { request.ToAddress },
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true
            };

            using var smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _settings.UseDefaultCredentials,
                EnableSsl = _settings.EnableSsl,
                Host = _settings.Host,
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password)
            };

            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }
    }
}
