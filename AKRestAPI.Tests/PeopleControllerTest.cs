using System;
using System.Xml;
using Xunit;
using AKRestAPI;
using AKRestAPI.Models;
using AKRestAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AKWebAPI.Tests
{
    public class PeopleControllerTests
    {
        [Fact]
        public void DefaultTest()
        {
            // Arrange
            var testPeopleRepository = new TestPeopleRepository();
            PeopleController testPeopleController = new PeopleController(testPeopleRepository);
            var expected = typeof(OkObjectResult);

            // Act
            var actual = testPeopleController.Get();

            // Assert
            Assert.IsType(expected, actual);
        }

        [Fact]
        public void findByNameTest()
        {
            // Arrange
            var testPeopleRepository = new TestPeopleRepository();
            PeopleController testPeopleController = new PeopleController(testPeopleRepository);
            var expected = typeof(JsonResult);

            // Act
            var actual = testPeopleController.Get("Joe", "Smith");

            // Assert
            Assert.IsType(expected, actual);
        }

        [Fact]
        public void findByAddressTest()
        {
            // Arrange
            var testPeopleRepository = new TestPeopleRepository();
            PeopleController testPeopleController = new PeopleController(testPeopleRepository);
            var expected = typeof(JsonResult);

            // Act
            var actual = testPeopleController.Get("336 FAKE ST UNIT 114 HENRY'S HOUSE");

            // Assert
            Assert.IsType(expected, actual);
        }

        [Fact]
        public void findByProviderTest()
        {
            // Arrange
            var testPeopleRepository = new TestPeopleRepository();
            PeopleController testPeopleController = new PeopleController(testPeopleRepository);
            var expected = typeof(JsonResult);

            // Act
            var actual = testPeopleController.Get("Provder Name");

            // Assert
            Assert.IsType(expected, actual);
        }
    }

    public class TestPeopleRepository : IPeopleRepository
    {

        public XmlNodeList findByAddress(string address)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(RawSoapResponse);
            return doc.GetElementsByTagName("SearchResponsePerson");
        }

        public XmlNodeList findByName(string firstName, string lastName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(RawSoapResponse);
            return doc.GetElementsByTagName("SearchResponsePerson");
        }

        public XmlNodeList findByProvider(string firstName, string lastName, string businessName, string speciality, string npi)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(RawSoapResponse);
            return doc.GetElementsByTagName("SearchResponsePerson");
        }

        private readonly string RawSoapResponse = @"
            <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
            <s:Body>
                <ns0:SearchResponse xmlns:ns0=""http://dhss.alaska.gov/schemas/searchPerson/2014-01"">
                <SearchResponsePerson>
                    <VirtualId>XXXXXXX</VirtualId>
                    <MatchPercentage></MatchPercentage>
                    <Title></Title>
                    <FirstName>John</FirstName>
                    <MiddleName>Q</MiddleName>
                    <LastName>Public</LastName>
                    <Suffix></Suffix>
                    <DateOfBirth>1980-10-21T00:00:00.000</DateOfBirth>
                    <Gender>Male</Gender>
                    <Registrations>
                    <Registration>
                        <RegistrationName>ARIES_ID</RegistrationName>
                        <RegistrationValue>XXXXXXXXXX</RegistrationValue>
                    </Registration>
                    </Registrations>
                    <Names>
                    <Name>
                        <NameType>Registered</NameType>
                        <Title></Title>
                        <FirstName>John</FirstName>
                        <MiddleName>Q</MiddleName>
                        <LastName>Allen</LastName>
                        <Suffix>Public</Suffix>
                    </Name>
                    </Names><Addresses/></SearchResponsePerson>
                <SearchResponsePerson>
                    <VirtualId>XXXXXXX</VirtualId>
                    <MatchPercentage></MatchPercentage>
                    <Title></Title>
                    <FirstName>JANE</FirstName>
                    <MiddleName>Q</MiddleName>
                    <LastName>PUBLIC</LastName>
                    <Suffix></Suffix>
                    <DateOfBirth>1970-05-21T00:00:00.000</DateOfBirth>
                    <Gender>Female</Gender>
                    <Registrations>
                    <Registration>
                        <RegistrationName>EIS_ID</RegistrationName>
                        <RegistrationValue>XXXXXXXXXX</RegistrationValue>
                    </Registration>
                    <Registration>
                        <RegistrationName>MediCaid</RegistrationName>
                        <RegistrationValue>XXXXXXXXXX</RegistrationValue>
                    </Registration>
                    <Registration>
                        <RegistrationName>SSN</RegistrationName>
                        <RegistrationValue>XXXXXXXXXX</RegistrationValue>
                    </Registration>
                    </Registrations>
                    <Names>
                    <Name>
                        <NameType>Registered</NameType>
                        <Title></Title>
                        <FirstName>JANE</FirstName>
                        <MiddleName>Q</MiddleName>
                        <LastName>PUBLIC</LastName>
                        <Suffix></Suffix>
                    </Name>
                    </Names>
                    <Addresses>
                    <Address Type=""Matching"" Value=""336 FAKE ST UNIT 114 HENRY'S HOUSE"">
                        <LocationElement>
                        <Type>HouseNameNumber</Type>
                        <Value>336 FAKE ST</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>HouseNameNumber</Type>
                        <Value></Value>
                        </LocationElement>
                    </Address>
                    <Address Type=""Mailing Address"" Value=""336 FAKE ST SITKA AK"">
                        <LocationElement>
                        <Type>Address Line</Type>
                        <Value>336 FAKE ST</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>City</Type>
                        <Value>SITKA</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>State</Type>
                        <Value>AK</Value>
                        </LocationElement>
                    </Address>
                    <Address Type=""Physical Address"" Value=""UNIT 114 HENRY'S HOUSE ANCHORAGE AK 99501"">
                        <LocationElement>
                        <Type>Address Line</Type>
                        <Value>UNIT 114</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>Address Line</Type>
                        <Value>HENRY'S HOUSE</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>City</Type>
                        <Value>ANCHORAGE</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>State</Type>
                        <Value>AK</Value>
                        </LocationElement>
                        <LocationElement>
                        <Type>ZIP</Type>
                        <Value>99501</Value>
                        </LocationElement>
                    </Address>
                    </Addresses>
                </SearchResponsePerson>
                </ns0:SearchResponse>
            </s:Body>
            </s:Envelope>";
    }
}
