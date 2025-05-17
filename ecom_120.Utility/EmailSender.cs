﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ecom_120.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;
        private readonly EmailSettings _emailSettings;
        public EmailSender (IConfiguration _configuration, IOptions<EmailSettings> emailSettings)
        {
            _configuration = configuration;
            _emailSettings = emailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Execute(email,subject,htmlMessage).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Ecom_120 Shopping App")
                };

                mailMessage.To.Add(toEmail);
                mailMessage.CC.Add(_emailSettings.CcEmail);
                mailMessage.Subject = "Shopping App: " + subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (SmtpClient smtpClient = new SmtpClient
                    (_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtpClient.Credentials = new NetworkCredential(
                        _emailSettings.UsernameEmail,
                        _emailSettings.UsernamePassword
                    );

                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
               string str=ex.Message;
            }
        }

    }
}
