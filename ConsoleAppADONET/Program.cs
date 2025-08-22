using ClassLibrary;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;



class Program
{
    static void Main(string[] args)
    {

        Type type = typeof(Subject);

        var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute;

        string tableName = tableAttribute.Name;

        string createTable = $"Create Table {tableName}(";

        foreach (var prop in type.GetProperties())
        {
            var columnAttribute = prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            if (columnAttribute != null)
            {
                Console.WriteLine($"Column Name: {prop.Name}, Type: {columnAttribute.Name}");

                createTable += $" {prop.Name} {columnAttribute.Name},";

            }
            else
            {
                Console.WriteLine($"Property: {prop.Name}, No Column Attribute");
            }
        }

        string finalQuery = createTable.TrimEnd(',',' ') + ")";

        Console.WriteLine(finalQuery);

        string connectionString = @$"Server=;Database= simpleDatabase;Trusted_Connection=True;TrustServerCertificate=True";

        using SqlConnection conn = new SqlConnection(connectionString);

        try
        {
            conn.Open();

            //string insertQuery = "insert into Subject values(1,'OS','interface between user and computer hardware','465h')";

            //string selectQuery = "select * from Subject";

            //SqlDataReader reader = cmd.ExecuteReader();

            //while( reader.Read() )
            //{
            //    Console.WriteLine($"{reader["Id"]}  {reader["Name"]} {reader["Description"]} {reader["Code"]} "); 
            //}

            using SqlCommand cmd = new SqlCommand(finalQuery, conn);

            Console.WriteLine(cmd.ExecuteNonQuery());
            Console.WriteLine("Table is created Successfully");


        }
        catch (Exception ex) {
           
            Console.WriteLine($"Message : {ex.Message}");

        }
    }
}