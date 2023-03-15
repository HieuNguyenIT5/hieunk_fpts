using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace WebAPI;

public class BuyerProceduce : IProceduceModel
{
    public bool Create(OracleConnection connection, Buyer buyer)
    {
        try
        {
            connection.Open();
            var command = new OracleCommand("addBuyer", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Input).Value = buyer.id;
            command.Parameters.Add("name", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.name;
            command.Parameters.Add("paymentMethod", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.paymentmethod;
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Update(OracleConnection connection, Buyer buyer)
    {
        connection.Open();
        var command = new OracleCommand("updateBuyer", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Input).Value = buyer.id;
        command.Parameters.Add("name", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.name;
        command.Parameters.Add("paymentMethod", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.paymentmethod;
        command.Parameters.Add("success", OracleDbType.Int32, ParameterDirection.Output);
        // Thực thi stored procedure
        command.ExecuteNonQuery();
        // Lấy giá trị trả về từ output parameter
        int success = ((OracleDecimal)command.Parameters["success"].Value).ToInt32();
        connection.Close();
        if (success == 0)
        {
            return false;
        }
        return true;
    }
    public bool Delete(OracleConnection connection, int id)
    {
        connection.Open();
        var command         = new OracleCommand("deleteBuyer", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("id", OracleDbType.Varchar2, ParameterDirection.Input).Value = id;
        command.Parameters.Add("out_success", OracleDbType.Int32, ParameterDirection.Output);
        command.ExecuteNonQuery();

        int success = ((OracleDecimal)command.Parameters["out_success"].Value).ToInt32();
        connection.Close();
        return success == 1 ? true : false;
    }
    public string Get(OracleConnection connection)
    {
        connection.Open();
        var command = new OracleCommand("getBuyers", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("result", OracleDbType.RefCursor, ParameterDirection.Output);
        var reader = command.ExecuteReader();
        var dataTable = new DataTable();
        dataTable.Load(reader);
        var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        connection.Close();
        return json;
    }
}
