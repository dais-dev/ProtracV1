using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Protrac1.Models;
using ProtracV1.Data;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Net.Sockets;
using MailKit.Security;
using MailKit.Net;



namespace ProtracV1.Services
{

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

        var mimeMessage = new MimeMessage();
          mimeMessage.From.Add(new MailboxAddress(emailSettings.SenderName, emailSettings.Sender));
          mimeMessage.To.Add(new MailboxAddress("", email));
          mimeMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
          mimeMessage.Body = bodyBuilder.ToMessageBody();

    ////
    // Chnged:::In Emailsettings (appsetting.json).Password  actually contains AppPassword set in gmail. after 2 veriification
        using (var client = new MailKit.Net.Smtp.SmtpClient())
         {
            await client.ConnectAsync(emailSettings.MailServer, emailSettings.MailPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.Sender, emailSettings.Password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
         }
    }
}

}
