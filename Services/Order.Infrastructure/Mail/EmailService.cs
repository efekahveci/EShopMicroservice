
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Order.Application.Contracts.Infrastructure;
using Order.Application.Models;

namespace Order.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _config;

    public EmailService(EmailSettings email)
    {
        _config = email;
    }


    //public Task<bool> SendEmail(Email request)
    //{
    //    var email = new MimeMessage();
    //    email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
    //    email.To.Add(MailboxAddress.Parse(request.To));
    //    email.Subject = request.Subject;
    //    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

    //    using var smtp = new SmtpClient();
    //    smtp.Connect(_config.GetSection("EmailHost").Value, Convert.ToInt32(_config.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
    //    smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
    //    smtp.Send(email);
    //    smtp.Disconnect(true);

    //    return Task.FromResult(true);
    //}

    public Task<bool> SendEmail(Email request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.EmailUsername));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.EmailHost, _config.EmailPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.EmailUsername, _config.EmailPassword);
        smtp.Send(email);
        smtp.Disconnect(true);

        return Task.FromResult(true);
    }
}
