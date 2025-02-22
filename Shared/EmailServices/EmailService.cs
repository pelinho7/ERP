using System.Threading.Tasks;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        string _senderAddress, _senderPassword;
        public EmailService(/*string senderAddress,string senderPassword*/)
        {
            _senderAddress = ""; //senderAddress;
            _senderPassword = ""; //senderPassword;
        }

        public async Task<(bool,string)> SendEmailAsync(string recevierEmail,string subject,string body){
            return await Task.Run(()=>SendEmail(recevierEmail,subject,body));
        }

        public (bool,string) SendEmail(string recevierEmail,string subject,string body)
        {
            try
            {
                // Credentials
                var credentials = new System.Net.NetworkCredential(_senderAddress, _senderPassword);
                // Mail message
                var mail = new System.Net.Mail.MailMessage()
                {
                    From = new System.Net.Mail.MailAddress($"noreply{_senderAddress}"),
                    Subject = subject,
                    Body = body
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new System.Net.Mail.MailAddress(recevierEmail));
                // Smtp client
                var client = new System.Net.Mail.SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
                return (true, null);
            }
            catch (System.Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}