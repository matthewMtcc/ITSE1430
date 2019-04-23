/*
 * Matthew McNatt
 * ITSE 1430
 * Last Modified 4/17/2019
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nile;
using Nile.Stores;

namespace Niles.Stores.Sql
{
    /// <summary>Implementation of the Product Database class using a Sql database</summary>
    public class SqlProductDatabase : ProductDatabase
    {
        #region Construction
        /// <summary>Constructor that accepts a connection string to a Sql Database</summary>
        public SqlProductDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Protected Members

        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        protected override Product AddCore( Product product )
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "AddProduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //adds paramaters with procedures
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                var result = Convert.ToInt32(cmd.ExecuteScalar());
                product.Id = result;
                return product;
            };

        }

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        protected override IEnumerable<Product> GetAllCore()
        {
            var ds = new DataSet();

            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }

            var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
            if (table != null)
            {
                return from r in table.Rows.OfType<DataRow>()
                       select new Product() {
                           Id = Convert.ToInt32(r["Id"]),
                           Name = r["Name"].ToString(),
                           Description = r.IsNull("description") ? "" : r["description"].ToString(),
                           Price = r.Field<decimal>("Price"),
                           IsDiscontinued = r.Field<bool>("isDiscontinued"),
                       };
            };

            return Enumerable.Empty<Product>();
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        protected override Product GetCore( int id )
        {
            using (var conn = GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "GetProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    var productId = reader.GetInt32(0);

                    if (productId == id)
                    {
                        return new Product {
                            Id = productId,
                            Name = GetString(reader, "Name"),
                            Description = GetString(reader, "Description"),
                            Price = reader.GetFieldValue<decimal>(reader.GetOrdinal("Price")),
                            IsDiscontinued = Convert.ToBoolean(reader.GetValue(reader.GetOrdinal("IsDiscontinued"))),
                        };
                    };
                };
            };
            return null;
        }

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        protected override void RemoveCore( int id )
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "RemoveProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

            };
        }

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        protected override Product UpdateCore( Product existing, Product newItem )
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "UpdateProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", newItem.Name);
                cmd.Parameters.AddWithValue("@description", newItem.Description);
                cmd.Parameters.AddWithValue("@price", newItem.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", newItem.IsDiscontinued);
                cmd.Parameters.AddWithValue("@id", existing.Id);

                cmd.ExecuteNonQuery();

            };

            return newItem;
        }
        #endregion

        #region Private Members
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private string GetString( IDataReader reader, string name )
        {
            var ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
                return "";

            return reader.GetString(ordinal);
        }

        private readonly string _connectionString;
        #endregion
    }
}
