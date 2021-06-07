using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace IDPDemoApp.Api.Data
{
    public class ProductsRepository
    {
        private readonly IDbConnection _db;

        //ctor
        public ProductsRepository(string connectionString)
        {
            _db = new NpgsqlConnection(connectionString);
        }

        public List<dynamic> GetAll()
        {
            return _db.Query("SELECT * FROM products").ToList();
        }
    }
}
