using System.Xml;

namespace AKRestAPI.Models
{
    public interface IPeopleRepository
    {
        XmlNodeList findByAddress(string address);
        XmlNodeList findByName(string firstName, string lastName);
        XmlNodeList findByProvider(string firstName, string lastName, string businessName, string speciality, string npi);
    }
}