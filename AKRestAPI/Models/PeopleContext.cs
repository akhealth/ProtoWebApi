using System.Xml;
using AK.connectors;

namespace AKRestAPI.Models {
    public class PeopleContext
    {
         private static string _endpoint;
        public PeopleContext(string endpoint)
        {
            _endpoint = endpoint;
        }

        public XmlNodeList makeSoapCall(string payload)
        {
             // Instantiate a new instance of class to call MCI service.
            SoapConnector soapConnector = new SoapConnector(_endpoint);

            // Get XML response from MCI service.
            XmlDocument SoapRespone = soapConnector.makeCall(payload);
            XmlNodeList searchResponsePerson = SoapRespone.GetElementsByTagName("SearchResponsePerson");

            // Serialize response as JSON.
            return searchResponsePerson;
        }
    }
}