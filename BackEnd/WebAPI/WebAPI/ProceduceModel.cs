using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using WebAPI.Models;

namespace WebAPI
{
    public interface IProceduceModel<T>
    {
        public bool Create(OracleConnection connection, T obj);
        public bool Update(OracleConnection connection, T obj);
        public bool Delete(OracleConnection connection, int id);
        public string Get(OracleConnection connection);
    }
}
