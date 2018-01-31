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
      string Endpoint = "https://esbtest.dhss.alaska.gov/aries/client/2400030560"; 
      string Result = "";

      try
      {
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
      string Endpoint = "https://esbtest.dhss.alaska.gov/eis/client/0600038118"; 
      string Result = "";

      try
      {
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

    public ContentResult MCI()
    {
        string Endpoint = "https://esbtest.dhss.alaska.gov/mci/person/search/";
        var Request = new StringContent("{ \"Registration\" : \"0600100001\" }", System.Text.UnicodeEncoding.UTF8, "application/json");
        string Result = "";

        try
        {
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