using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.App.Common;
public class Constants
{
    //Send Mail
    public const string BEGINBODY = "<table>" +
                    "<tr>" +
                    "<th>Tên hàng</th><th>Số lượng</th><th>Giá</th><th>Tổng</th>" +
                    "</tr>";
    public const string ENDBODY = "</ table > " +
                    "<style>" +
                    "td,th{border: 1px solid black; padding:5px;}" +
                    "table{border: 1px solid black;border-collapse: collapse;}" +
                    "</style>";
    public string addItem(string ProductName, int Quantity, decimal Price, decimal SubTotal)
    {
        return "<tr>" +
               "<td>" + ProductName + "</td>" +
               "<td>" + Quantity + "</td>" +
               "<td>" + Price + "</td>" +
               "<td>" + SubTotal + "</td>" +
               "</tr>";
    }
    public string addTotalCash(decimal totalCash)
    {
        return "<tr>" +
               "<td colspan='3'>Tổng tiền:</td>" +
               "<td>" + totalCash + "</td>" +
               "</tr>";
    }

}
