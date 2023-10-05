using BookingAP.Application.Abstractions.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookingAP.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
