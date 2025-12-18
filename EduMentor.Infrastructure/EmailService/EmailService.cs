using EduMentor.Application.Interfaces.Email;
using EduMentor.Domain.EmailModel;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EduMentor.Infrastructure.EmailService;

public class EmailService(IOptions<SmtpSettings> smtp) : IEmailService
{
    private readonly SmtpSettings _smtp = smtp.Value;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("EduMentor", _smtp.Username));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("html") { Text = body };

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_smtp.Host, _smtp.Port, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_smtp.Username, _smtp.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}