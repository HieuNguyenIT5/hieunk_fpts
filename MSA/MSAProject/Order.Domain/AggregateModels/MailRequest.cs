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

        public void AddItem(string ProductName, int Quantity, decimal Price, decimal SubTotal)
        {
            this.Body += "<tr>" +
                   "<td>" + ProductName + "</td>" +
                   "<td>" + Quantity + "</td>" +
                   "<td>" + Price + "</td>" +
                   "<td>" + SubTotal + "</td>" +
                   "</tr>";
        }
        public void AddTotalCash(decimal totalCash)
        {
            this.Body += "<tr>" +
                   "<td colspan='3'>Tổng tiền:</td>" +
                   "<td>" + totalCash + "</td>" +
                   "</tr>";
        }
    }
}
