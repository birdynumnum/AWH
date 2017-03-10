using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.MailProvider
{
    public interface IMailer
    {
        void SendMail(string from, string to, string subject, string username, string pw);
    }
}
