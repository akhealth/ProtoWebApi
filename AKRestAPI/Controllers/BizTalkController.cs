using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace AKRestAPI.Controllers
{
  public class BizTalkController : Controller
  {

    public ContentResult Aries()
    {
      string result = "";

      try
      {
        string ariesId = HttpContext.Request.Query["id"].ToString();
        if (ariesId == "")
          throw new System.ArgumentException("Parameter ?id must be included");

        string endpoint = "https://esbtest.dhss.alaska.gov/aries/client/" + ariesId;

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = endpoint;
          var response = httpClient.GetAsync(uri).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          result = json;
        }

      }
      catch (Exception ex)
      {
        result = $"Problems connecting to: BizTalk/ARIES\r\n\r\n {ex}";
      }
      return Content(result, "application/json");
    }

    public ContentResult EIS()
    {
      string result = "";

      try
      {
        string eisId = HttpContext.Request.Query["id"].ToString();
        if (eisId == "")
          throw new System.ArgumentException("Parameter ?id must be included");

        string endpoint = "https://esbtest.dhss.alaska.gov/eis/client/" + eisId;

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = endpoint;
          var response = httpClient.GetAsync(uri).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          result = json;
        }

      }
      catch (Exception ex)
      {
        result = $"Problems connecting to: BizTalk/EIS\r\n\r\n {ex}";
      }
      return Content(result, "application/json");
    }

    public ContentResult MCI()
    {

      string result = "";

      try
      {
        string endpoint = "https://esbtest.dhss.alaska.gov/mci/person/search/";

        string regId = HttpContext.Request.Query["id"].ToString();
        string firstName = HttpContext.Request.Query["first"].ToString();
        string lastName = HttpContext.Request.Query["last"].ToString();
        var request = new StringContent("");

        if ((firstName == "" && lastName == "") && regId == "")
          throw new System.ArgumentException("Parameters (?first and ?last) OR ?id must be included");

        if (regId != "")
        {
          // {{ -> { in an interpolated string
          string data = $"{{ \"Registration\":\"{ regId }\" }}";
          request = new StringContent(data, System.Text.UnicodeEncoding.UTF8, "application/json");
          }
        else
        {
          string data = $"{{ \"FirstName\":\"{ firstName }\",\"LastName\" :\"{ lastName }\" }}";
          request = new StringContent(data, System.Text.UnicodeEncoding.UTF8, "application/json");
          }

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = endpoint;
          var response = httpClient.PostAsync(uri, request).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          result = json;
        }
      }
      catch (Exception ex)
      {
        result = $"Problems connecting to: BizTalk/MCI\r\n\r\n {ex}";
      }
      return Content(result, "application/json");
    }
  }
}