using productin.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace productin.DATA
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }
            _connectionString = connectionString;
        }

        // 1. Get All Products (Read)
        public IEnumerable<Product> GetAllProducts()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Product>("SELECT * FROM Products").ToList(); // Return a list
            }
        }

        // 2. Get Product By Id (Read)
        public Product? GetProductById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
            }
        }

        // 3. Add a New Product (Create)
        public void AddProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
                db.Execute(sqlQuery, product);
            }
        }

        // 4. Update an Existing Product (Update)
        public void UpdateProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                db.Execute(sqlQuery, product);
            }
        }

        // 5. Delete a Product (Delete)
        public void DeleteProduct(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Products WHERE Id = @Id";
                db.Execute(sqlQuery, new { Id = id });
            }
        }
    }
}