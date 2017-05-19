using System.Xml;
using SoapHttpClient;
using SoapHttpClient.Extensions;

namespace AK.connectors
{
    public class SoapConnector
    {
        // URL for SOAP endpoint.
        private string _endpoint;

        /*
         * Class constructor
         */
        public SoapConnector(string endpoint)
        {
            _endpoint = endpoint;
        }

        /*
         * Execute SOAP call with payload.
         */
        public XmlDocument makeCall(string payload)
        {

            // XML doc to hold the SOAP payload.
            XmlDocument body = new XmlDocument();
            body.LoadXml(payload);

            using (SoapClient soapClient = new SoapClient())
            {
                // Make call to SOAP endpoint.
                var result = soapClient.Post(endpoint: _endpoint, body: body);

                // XML doc to hold SOAP response.
                XmlDocument response = new XmlDocument();
                response.LoadXml(result.Content.ReadAsStringAsync().Result);

                // Return XML from SOAP service.
                return response;
            }
        }
    }
}