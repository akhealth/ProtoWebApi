using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace AKRestAPI.Controllers
{
  public class BizTalkController : Controller
  {

    public ContentResult Aries()
    {
      string Result = "";

      try
      {
        string AriesId = HttpContext.Request.Query["id"].ToString();
        if (AriesId == "")
          throw new System.ArgumentException("Parameter ?id must be included");

        string Endpoint = "https://esbtest.dhss.alaska.gov/aries/client/" + AriesId;

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = Endpoint;
          var response = httpClient.GetAsync(uri).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          Result = json;
        }

      }
      catch (Exception ex)
      {
        Result = $"Problems connecting to: BizTalk/ARIES\r\n\r\n {ex}";
      }
      return Content(Result, "application/json");
    }

    public ContentResult EIS()
    {
      string Result = "";

      try
      {
        string EisId = HttpContext.Request.Query["id"].ToString();
        if (EisId == "")
          throw new System.ArgumentException("Parameter ?id must be included");

        string Endpoint = "https://esbtest.dhss.alaska.gov/eis/client/" + EisId;

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = Endpoint;
          var response = httpClient.GetAsync(uri).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          Result = json;
        }

      }
      catch (Exception ex)
      {
        Result = $"Problems connecting to: BizTalk/EIS\r\n\r\n {ex}";
      }
      return Content(Result, "application/json");
    }

    // \"FirstName\":\"Major\",\"LastName\" :\"Snow\"
    // \"Registration\":\"0600100001\"

    public ContentResult MCI()
    {

      string Result = "";

      try
      {
        string Endpoint = "https://esbtest.dhss.alaska.gov/mci/person/search/";

        string RegId = HttpContext.Request.Query["id"].ToString();
        string FirstName = HttpContext.Request.Query["first"].ToString();
        string LastName = HttpContext.Request.Query["last"].ToString();
        var Request = new StringContent("");

        if ((FirstName == "" && LastName == "") && RegId == "")
          throw new System.ArgumentException("Parameters (?first and ?last) OR ?id must be included");

        if (RegId != "")
        {
          // {{ -> { in an interpolated string
          string data = $"{{ \"Registration\":\"{ RegId }\" }}";
          Request = new StringContent(data, System.Text.UnicodeEncoding.UTF8, "application/json");
          }
        else
        {
          string data = $"{{ \"FirstName\":\"{ FirstName }\",\"LastName\" :\"{ LastName }\" }}";
          Request = new StringContent(data, System.Text.UnicodeEncoding.UTF8, "application/json");
          }

        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
          var uri = Endpoint;
          var response = httpClient.PostAsync(uri, Request).Result;
          var json = response.Content.ReadAsStringAsync().Result;

          Result = json;
        }
      }
      catch (Exception ex)
      {
        Result = $"Problems connecting to: BizTalk/MCI\r\n\r\n {ex}";
      }
      return Content(Result, "application/json");
    }
  }
}