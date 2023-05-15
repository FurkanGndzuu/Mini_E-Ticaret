using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IMailService
    {
        Task SendMailAsync(string to , string subject , string body , bool isBodyHtml = false);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = false);
        Task OrderCompleteMailAsync(string to , string username , DateTime cretaedDate);
    }
}
