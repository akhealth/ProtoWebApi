## REST API For AK MCI

A .NET Core Web API application to support searching for Medicaid applicants in various backend systems. This is a prototype application that will asssit the ARIES product team in its initial procurement.

## Usage

To run this app on your local machine:
* Install .NET Core and VS Code with the C# extension.
* Open the `AKRestAPI` folder in VS Code
* If prompted, select *Yes* to the Warn message "Required assets to build and debug are missing from. Add them?"
* If prompted, select *Restore* to the Info message "There are unresolved dependencies".
* Open the integrated terminal (View > Integrated terminal)
* At the terminal prompt, run: `dotnet run`
* App should be available at `http://localhost:5000/mci/people`

To run a Docker image, first build the image:

```bash
~$ docker build -t {username}/akrestapi -f docker/Dockerfile .
```

Then run it:

```bash
~$ docker run -p 5000:5000 -d {username}/akrestapi
```

You should now be able to access the API at: `http://localhost:5000/mci/people`

## Running Tests

You can run tests by doing the following from the main project directory:

```bash
~$ dotnet test AKRestAPI.Tests/AKRestAPI.Tests.csproj
```

## Sample calls and responses

To search by name, invoke the API thusly:

```
127.0.0.1:5000/mci/people/findByName?firstName=Greg&lastName=Allen
```

Response:

```json

[
  {
    "SearchResponsePerson": {
      "VirtualId": "6960492",
      "MatchPercentage": "",
      "Title": "",
      "FirstName": "Greg",
      "MiddleName": "",
      "LastName": "Allen",
      "Suffix": "",
      "DateOfBirth": "1980-10-21T00:00:00.000",
      "Gender": "Male",
      "Registrations": {
        "Registration": {
          "RegistrationName": "ARIES_ID",
          "RegistrationValue": "XXXXXXXXXX"
        }
      },
      "Names": {
        "Name": {
          "NameType": "Registered",
          "Title": "",
          "FirstName": "Greg",
          "MiddleName": "",
          "LastName": "Allen",
          "Suffix": ""
        }
      },
      "Addresses": null
    }
  },
  {
    "SearchResponsePerson": {
      "VirtualId": "6974287",
      "MatchPercentage": "",
      "Title": "",
      "FirstName": "CRAIG",
      "MiddleName": "",
      "LastName": "ALLEN",
      "Suffix": "",
      "DateOfBirth": "1970-05-21T00:00:00.000",
      "Gender": "Male",
      "Registrations": {
        "Registration": [
          {
            "RegistrationName": "EIS_ID",
            "RegistrationValue": "XXXXXXXXXX"
          },
          {
            "RegistrationName": "MediCaid",
            "RegistrationValue": "XXXXXXXXXX"
          },
          {
            "RegistrationName": "SSN",
            "RegistrationValue": "XXXXXXXXX"
          }
        ]
      },
      "Names": {
        "Name": {
          "NameType": "Registered",
          "Title": "",
          "FirstName": "CRAIG",
          "MiddleName": "",
          "LastName": "ALLEN",
          "Suffix": ""
        }
      },
      "Addresses": {
        "Address": [
          {
            "@Type": "Matching",
            "@Value": "336 FAKE ST UNIT 200 HENRY'S HOUSE",
            "LocationElement": [
              {
                "Type": "HouseNameNumber",
                "Value": "336 FAKE ST"
              },
              {
                "Type": "HouseNameNumber",
                "Value": "UNIT 200 HENRY'S HOUSE"
              }
            ]
          },
          {
            "@Type": "Mailing Address",
            "@Value": "336 FAKE ST SITKA AK",
            "LocationElement": [
              {
                "Type": "Address Line",
                "Value": "336 FAKE ST"
              },
              {
                "Type": "City",
                "Value": "SITKA"
              },
              {
                "Type": "State",
                "Value": "AK"
              }
            ]
          },
          {
            "@Type": "Physical Address",
            "@Value": "UNIT 200 HENRY'S HOUSE ANCHORAGE AK 99501",
            "LocationElement": [
              {
                "Type": "Address Line",
                "Value": "UNIT 200"
              },200
              {
                "Type": "Address Line",
                "Value": "HENRY'S HOUSE"
              },
              {
                "Type": "City",
                "Value": "ANCHORAGE"
              },
              {
                "Type": "State",
                "Value": "AK"
              },
              {
                "Type": "ZIP",
                "Value": "99501"
              }
            ]
          }
        ]
      }
    }
  }
]
```



