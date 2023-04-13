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
   

}
