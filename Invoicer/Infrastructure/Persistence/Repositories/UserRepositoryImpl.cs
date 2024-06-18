using Dapper;
using Domain.DTOs.Request;
using Domain.Models;
using Infrastructure.Persistence.Mappers.Entities;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepositoryImpl : UserRepository
    {
        private Settings _settings;

        public UserRepositoryImpl(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        protected MySqlConnection DbConnection()
        {
            return new MySqlConnection(_settings.ServiceConfig.ConnectionString);
        }

        public async Task<UserEntity> GetUserByUsername(UserDto user)
        {
            var db = DbConnection();

            var sql = @"SELECT username, password
                        FROM user
                        WHERE username = @Username";

            return await db.QueryFirstOrDefaultAsync<UserEntity>(sql, new { Username = user.Username });
        }
    }
}
