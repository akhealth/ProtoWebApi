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