using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailService
    {
        (bool, string) SendEmail(string recevierEmail, string subject, string body);
        Task<(bool, string)> SendEmailAsync(string recevierEmail, string subject, string body);
    }
}