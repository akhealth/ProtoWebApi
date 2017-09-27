using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using Npgsql;

// Canned ARIES Postgres queries returned as JSON
namespace AKRestAPI.Controllers
{
  public class AriesController : Controller
  {
    private string transformToJsonQuery(string query)
    {
      string jsonQuery = "SELECT array_to_json(array_agg(row_to_json(t))) FROM ( ";
      jsonQuery += query;
      jsonQuery += " ) t";
      return jsonQuery;
    }

    private void validateParameters(string ids)
    {
      Regex comma_separated_integers = new Regex(@"^(\d+)(,\s*\d+)*$");

      if (!comma_separated_integers.IsMatch(ids))
        throw new Exception("input is comma separated list of integers");
    }

    public ContentResult Index()
    {
      // get parameters and do some not-amazing validation
      string idsToQuery = HttpContext.Request.Query["ids"].ToString();
      validateParameters(idsToQuery);

      // A real ARIES query. Try it with ids=2400127130,2400141779,2400120002,2400070731,2400144956,2400105462,2400149734,2400013380,2400062485,2400121159
      string AriesQuery = 
        "SELECT ar_app_indv.app_num, ar_application_for_aid.application_Status_cd, ar_app_indv.indv_id " +
        "FROM ar_application_for_aid " +
          "LEFT JOIN ar_app_indv " +
          "ON ar_application_for_aid.app_num = ar_app_indv.app_num " +
        $"WHERE ar_app_indv.indv_id IN ({idsToQuery})";
      AriesQuery = transformToJsonQuery(AriesQuery);

      string AriesResult = "";
      string connStr = AKRestAPI.Util.GetEnv("PgConnectionString");

      try
      {
        NpgsqlConnection conn = new NpgsqlConnection(connStr);
        conn.Open();
        NpgsqlCommand cmd = new NpgsqlCommand(AriesQuery, conn);

        // Execute a query
        NpgsqlDataReader dr = cmd.ExecuteReader();

        // Read and add all rows to result. (There _should_ be only one result here because of the JSON functions.)
        while (dr.Read())
          AriesResult += dr[0];

        // return [] on an empty result
        if(AriesResult == "")
          AriesResult = "[]";

        // Close connection
        conn.Close();

        return Content(AriesResult, "application/json");
      }
      catch (Exception ex)
      {
        return Content($"Problems connecting to: Postgres\r\n\r\n {ex}", "text/plain");
      }

    }

  }
}