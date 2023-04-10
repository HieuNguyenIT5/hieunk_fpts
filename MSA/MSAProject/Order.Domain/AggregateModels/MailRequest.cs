using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.App.Services
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailRequest(string ToEmail, string Subject, string Body)
        {
            this.ToEmail = ToEmail;
            this.Subject = Subject;
            this.Body = Body;
        }
    }
}
