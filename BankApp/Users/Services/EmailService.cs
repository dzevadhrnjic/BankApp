﻿using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BankApp.Users.Services
{
    public class EmailService : IEmailService
    {

        private readonly string emailFrom = "fromemail@gmail.com";
        public void SendEmail(string to, string subject, string body)
        {

            MailMessage message = new MailMessage(emailFrom, to);
            message.Subject = subject;
            message.Body = body;

            CreateSMTPClient(message);
        }

        public void SendEmailWithAttachment(string to, string subject, string body)
        {
           
            string file = "D:\\BankApp\\BankApp\\transactions.pdf";

            MailMessage message = new MailMessage(emailFrom, to);
            message.Subject = subject;

            Attachment attachment = new Attachment(file, MediaTypeNames.Application.Octet);
            message.Attachments.Add(attachment);

            CreateSMTPClient(message);
        }

        private void CreateSMTPClient(MailMessage message)
        {

            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "hrnjicdzevadd@gmail.com";
            string smtpPassword = "pfzvirxuctxmvule";

            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            smtpClient.Send(message);
        }

        public string GetRandomNumbers()
        {
            Random random = new Random();

            string numbers = random.Next(0, 1000000).ToString("D6");

            return numbers;
        }
    }
}
