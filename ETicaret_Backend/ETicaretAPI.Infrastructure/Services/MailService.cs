using ETicaretAPI.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = false)
        {
            MailMessage mail = new();
            mail.Subject = subject;
            mail.Body = body;
            mail.To.Add(to);
            mail.IsBodyHtml = isBodyHtml;
            mail.From = new(_configuration["Mail:userName"], "Furkan Commerce", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Mail:userName"], _configuration["Mail:password"]);
            smtp.Host = _configuration["Mail:host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = false)
        {
            foreach(string to in tos)
                await SendMailAsync(to, subject, body, isBodyHtml);
        }


        public async Task OrderCompleteMailAsync(string to, string username, DateTime createdDate)
        {
            string subject = "Order in Cargo";
            string body = $"Merhaba {username} <br>  Şu {createdDate} Tarihte Yaptığınız Sipariş An İtibariyle Kargoya Verilmiştir.<br>" +
                $"İyi Günler Dileriz...";
            await SendMailAsync(to, subject, body, true);
        }
    }
}
