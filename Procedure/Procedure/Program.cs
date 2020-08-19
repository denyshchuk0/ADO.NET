using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_AvarageMark", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    Console.WriteLine("Enter a subject");

                    command.Parameters.Add("@subject", SqlDbType.NVarChar, 30).Value = Console.ReadLine();

                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@avarageMark",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    });
                
                    command.ExecuteNonQuery();

                    Console.WriteLine($"Resault: {command.Parameters["@avarageMark"].Value}");

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
