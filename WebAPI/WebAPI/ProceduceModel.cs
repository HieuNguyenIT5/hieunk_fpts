using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace WebAPI
{
    public interface IProceduceModel
    {
        public bool Create(OracleConnection connection, object obj);
        public bool Update(OracleConnection connection, object obj);
        public bool Delete(OracleConnection connection, int id);
        public string Get(OracleConnection connection);
    }
}
