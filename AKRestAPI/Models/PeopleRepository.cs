using System;
using System.Xml;

namespace AKRestAPI.Models
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly PeopleContext _context;

        public PeopleRepository(PeopleContext context)
        {
            _context = context;
        }
        public XmlNodeList findByAddress(string address)
        {
            string payload = findByAddressPayload(address);
            return _context.makeSoapCall(payload);
        }
        public XmlNodeList findByName(string firstName, string lastName)
        {
            string payload = findByNamePayload(firstName, lastName);
            return _context.makeSoapCall(payload);
        }
        public XmlNodeList findByProvider(string firstName, string lastName, string businessName, string speciality, string npi)
        {
            string payload = findByProviderPayload(firstName, lastName, businessName, speciality, npi);
            return _context.makeSoapCall(payload);
        }

        private static string findByAddressPayload(string address)
        {
            // A template to use for searches
            string bodyTemplate = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:mci=""http://MCI.PersonService.SearchByAddress"">
            <soapenv:Header/>
            <soapenv:Body>
                <mci:AddressSearch>
                    <Address>{0}</Address>
                </mci:AddressSearch>
            </soapenv:Body>
            </soapenv:Envelope>";
            return String.Format(bodyTemplate, address);
        }

        private static string findByNamePayload(string firstName, string lastName)
        {
            string bodyTemplate = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns=""http://dhss.alaska.gov/schemas/searchPerson/2014-01"">
            <soapenv:Header/>
                <soapenv:Body>
                    <ns:Search SearchType =""?"">
                        <FirstName>{0}</FirstName>
                        <LastName>{1}</LastName>
                    </ns:Search>
                </soapenv:Body>
            </soapenv:Envelope>";
            return String.Format(bodyTemplate, firstName, lastName);
        }

        private static string findByProviderPayload(string firstName, string lastName, string businessName, string speciality, string npi)
        {
            string bodyTemplate = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:mci=""http://MCI.PersonService.SearchProvider"">
            <soapenv:Header/>
            <soapenv:Body>
                <mci:SearchProviderRequest>
                    <FirstName>{0}</FirstName>
                    <LastName>{1}</LastName>
                    <BusinessName>{2}</BusinessName>
                    <Speciality>{3}</Speciality>
                    <NPI>{4}</NPI>
                </mci:SearchProviderRequest>
            </soapenv:Body>
            </soapenv:Envelope>";
            return String.Format(bodyTemplate, firstName, lastName, businessName, speciality, npi);
        }
    }

}