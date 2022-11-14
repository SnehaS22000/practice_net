using Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository

    {
        private readonly IConfiguration _configuration;
        protected string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection").ToString();
        }
        protected async Task<DataTable> ExecuteDataTable(string sqlCommandText)
        {
            using MySqlConnection con = new(_connectionString);
            using MySqlCommand cmd = new(sqlCommandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            using MySqlDataAdapter dataAdapter = new(cmd);
            DataTable dt = new();
            await dataAdapter.FillAsync(dt);
            return dt;

        }

        protected async Task<DataTable> ExecuteDataTable(string sqlCommandText, List<MySqlParameter> parameters)
        {
            using MySqlConnection con = new(_connectionString);
            using MySqlCommand cmd = new(sqlCommandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            using MySqlDataAdapter dataAdapter = new(cmd);
            DataTable dt = new();
            await dataAdapter.FillAsync(dt);
            return dt;

        }
        protected async Task<MySqlDataReader> ExecuteDataReader(string sqlCommandText, List<MySqlParameter> parameters)
        {
            using MySqlConnection con = new(_connectionString);
            using MySqlCommand cmd = new(sqlCommandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            await con.OpenAsync();
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            MySqlDataReader reader = cmd.ExecuteReader();          
            await con.CloseAsync();
            return reader;


        }
        protected async Task<int> ExecuteNonQuery(string sqlCommandText, List<MySqlParameter> parameters)
        {
            using MySqlConnection con = new(_connectionString);
            using MySqlCommand cmd = new(sqlCommandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            await con.OpenAsync();
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            var result=cmd.ExecuteNonQuery();
            await con.CloseAsync();
            return result;


        }
    }
}
