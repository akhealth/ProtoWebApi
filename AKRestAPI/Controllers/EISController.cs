using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Xml;
using Newtonsoft.Json;

// EIS Web Service exists on-prem in Alaska. This controller acts as a pass-through from the front-end to the hybrid connection.
// Requests to this controller pass in `id`: http://localhost:5000/eis?id=0600093208
namespace AKRestAPI.Controllers
{
  public class EISController : Controller
  {

    public ContentResult Index()
    {
      string EISEndpoint = AKRestAPI.Util.GetEnv("EISEndpoint");
      string EISResult = "";

      try
      {
        var handler = new System.Net.Http.HttpClientHandler();
        using (var httpClient = new HttpClient(handler))
        {
            //TODO: This line makes us ignore certificate warnings. It works in Azure but not on OSX
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            var uri = EISEndpoint + HttpContext.Request.Query["id"].ToString();
            var response = httpClient.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            XmlDocument xml_doc = new XmlDocument();
            xml_doc.LoadXml(result);
            string json_result = JsonConvert.SerializeXmlNode(xml_doc);

            if (json_result.Contains("Client not found"))
              return new ContentResult() { Content = "Not Found", StatusCode = 404 };
            
            EISResult = json_result;
        }
        
      }
      catch (Exception ex)
      {
        EISResult = $"Problems connecting to: EIS Web Service\r\n\r\n {ex}";
      }
      return Content(EISResult, "application/json");
    }

  }
}