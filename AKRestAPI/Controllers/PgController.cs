using Microsoft.AspNetCore.Mvc;
using System;
using Npgsql;

// A Postgres example
namespace AKRestAPI.Controllers
{
  public class PgController : Controller
  {

    public string Index()
    {

      string PGResult = "";
      string connStr = AKRestAPI.Util.GetEnv("PgConnectionString");
      

      try
      {
        NpgsqlConnection conn = new NpgsqlConnection(connStr);
        conn.Open();
        NpgsqlCommand cmd = new NpgsqlCommand(AKRestAPI.Util.GetEnv("PgQuery"), conn);

        // Execute a query
        NpgsqlDataReader dr = cmd.ExecuteReader();

        // Read all rows
        while (dr.Read())
          PGResult += $"{dr[0]} : {dr[1]} : {dr[2]}\r\n";

        // Close connection
        conn.Close();
      }
      catch (Exception ex)
      {
        PGResult = $"Problems connecting to: Postgres\r\n\r\n {ex}";
      }

      return PGResult;
    }

  }
}