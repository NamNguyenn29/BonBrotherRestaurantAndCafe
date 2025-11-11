using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using RCBE.DTO;
namespace RCBE.Helper.SendMail.Implements

{
    public class MailImpl : RCBE.Helper.SendMail.IMailService
    {
        private readonly EmailSetting _emailSetting;

        public MailImpl(IOptions<EmailSetting> options)
        {
            _emailSetting = options.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSetting.Username));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            try
            {
                var socketOption = _emailSetting.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.SslOnConnect;

                await client.ConnectAsync(_emailSetting.SmtpServer, _emailSetting.Port, socketOption);
                await client.AuthenticateAsync(_emailSetting.Username, _emailSetting.Password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (client.IsConnected) await client.DisconnectAsync(true);
            }
        }


    }
}
