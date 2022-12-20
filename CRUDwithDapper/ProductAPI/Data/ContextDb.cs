using Dapper;
using ProductAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductAPI.Data
{
    public class ContextDb
    {

        private readonly string conStr = "Server=DESKTOP-0H0C8LT\\SQLEXPRESS;Database=ProductDb;Trusted_Connection=True;";

        public Product GetProduct(int id)
        {
            Product product =new();
            string query = "GetPrductById";
            using (SqlConnection con = new(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductId", id));
                    SqlDataAdapter adp = new(cmd);
                    DataTable dt = new();
                    adp.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                         product = new Product { 
                        
                            ProductId = Convert.ToInt32(dr[0]),
                            ProductName = Convert.ToString(dr[1]),
                            ProductDescription = Convert.ToString(dr[2]),
                            ProductPrice = Convert.ToInt32(dr[3]),
                            ProductStock = Convert.ToInt32(dr[4]),
                        };
                    }
                }
            }
            return product;
        }


        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string query = "GetPrductList";
            using (SqlConnection con = new(conStr))
            {
                con.Open();
                    products = con.Query<Product>(query,
                         commandType: CommandType.StoredProcedure).ToList();
                
            }
            return products;
        }

        public bool AddProduct(ProductDTO product)
        {

            string query = "AddNewProduct";
            using (SqlConnection con = new(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                    cmd.Parameters.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
                    cmd.Parameters.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
                    cmd.Parameters.Add(new SqlParameter("@ProductStock", product.ProductStock));
                    int result= cmd.ExecuteNonQuery();

                    con.Close();
                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool EditProduct(int id, ProductDTO product)
        {
            string query = "UpdateProduct";
            using (SqlConnection con = new(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new(query, con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductId", id));
                    cmd.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                    cmd.Parameters.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
                    cmd.Parameters.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
                    cmd.Parameters.Add(new SqlParameter("@ProductStock", product.ProductStock));
                    int result = cmd.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool DeleteProduct(int id)
        {
            string query = "DeletePrductByID";
            using (SqlConnection con = new(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductId", id));
                    cmd.ExecuteNonQuery();
                    int result = cmd.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
