using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Comics.Data.Repository
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<BaseRepository> _logger;

        protected BaseRepository(IConfiguration configuration, ILogger<BaseRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        // use for buffered queries that return a type
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return await getData(connection);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
                throw;
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
                throw;
            }
        }

        // use for buffered queries that do not return a type
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                await getData(connection);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
                throw;
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
                throw;
            }
        }

        //use for non-buffered queries that return a type
        protected async Task<TResult> WithConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData, Func<TRead, Task<TResult>> process)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var data = await getData(connection);
                return await process(data);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
                throw;
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
                throw;
            }
        }
    }
}

