using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

// A Simple SQL Server example
namespace AKRestAPI.Controllers
{
  public class SqlController : Controller
  {

    public string Index()
    {
      string connStr = AKRestAPI.Util.GetEnv("SqlConnectionString");
      string SQLResult = "";

      try
      {
        using (var connection = new SqlConnection(connStr))
        {
          var command = new SqlCommand(AKRestAPI.Util.GetEnv("SqlQuery"), connection);
          connection.Open();
          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
              SQLResult += $"{reader[0]} : {reader[1]} : {reader[2]}\r\n";
          }
        }
      }
      catch (Exception ex)
      {
        SQLResult = $"Problems connecting to: SQL Server\r\n\r\n {ex}";
      }
      return SQLResult;
    }

  }
}