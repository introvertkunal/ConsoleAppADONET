using ClassLibrary;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Type type = typeof(Subject);

        // Get SqlTable attribute instead of [Table]
        var tableAttribute = type.GetCustomAttributes(typeof(SqlTableAttribute), false).FirstOrDefault() as SqlTableAttribute;

        if (tableAttribute == null)
        {
            Console.WriteLine("No SqlTable attribute found.");
            return;
        }

        string tableName = tableAttribute.TableName;
        string createTable = $"CREATE TABLE {tableName} (";

        List<string> indexQueries = new List<string>();

        foreach (var prop in type.GetProperties())
        {
            var columnAttribute = prop.GetCustomAttributes(typeof(SqlColumnAttribute), false).FirstOrDefault() as SqlColumnAttribute;

            if (columnAttribute != null)
            {
                string columnName = prop.Name;
                string sqlType = columnAttribute.SqlType;

                string constraints = "";

                if (columnAttribute.ISPrimaryKey)
                    constraints += " PRIMARY KEY";

                if (columnAttribute.IsAutoIncremented)
                    constraints += " IDENTITY(1,1)";

                if (!columnAttribute.ISNullable)
                    constraints += " NOT NULL";

                createTable += $" {columnName} {sqlType}{constraints},";

                // Handle index
                if (columnAttribute.IsIndexed)
                {
                    string indexQuery = $"CREATE INDEX IDX_{tableName}_{columnName} ON {tableName}({columnName});";
                    indexQueries.Add(indexQuery);
                }
            }
            else
            {
                Console.WriteLine($"Property: {prop.Name}, No SqlColumn attribute found");
            }
        }

        string finalQuery = createTable.TrimEnd(',') + ")";

        Console.WriteLine("Generated CREATE TABLE:");
        Console.WriteLine(finalQuery);

        foreach (var q in indexQueries)
        {
            Console.WriteLine("Generated INDEX:");
            Console.WriteLine(q);
        }

        string connectionString = @"Server=localhost;Database=simpleDatabase;Trusted_Connection=True;TrustServerCertificate=True";

        using SqlConnection conn = new SqlConnection(connectionString);

        try
        {
            conn.Open();

            // Execute CREATE TABLE
            using (SqlCommand cmd = new SqlCommand(finalQuery, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Table created successfully.");
            }

            // Execute CREATE INDEX
            foreach (var idxQuery in indexQueries)
            {
                using SqlCommand cmd = new SqlCommand(idxQuery, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Index created: " + idxQuery);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }
}
