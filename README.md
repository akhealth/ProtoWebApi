# Prototype

An ASP.NET Core 2.0 web application that supports searching for Medicaid applicants.
This is a prototype application that will assist the ARIES product team in its initial procurement.

This project primarily consists of a REST API for the Alaska MCI SOAP service and a web front-end.
It also consists of Proof Of Concept for SQL Server and Postgres connections.

## Environment

This app relies on `ENV` variables for configuration. Copy the example file and customize.

```sh
cd AKRestAPI
cp .env.example.bash .env
source .env
```

**Note**: `.env.example.bash` and `.env.example.ps1` already has correct values for the `docker-compose` setup.  Edit `.env` if you have to.

## Running locally

We prefer using Docker locally. You can run the application with the `dotnet` CLI if you wish, but you're on your own for SQL Server and Postgres.

```bash
docker-compose build
docker-compose up -d
./sql-seed/seed-local-databases.sh
```

- If you make changes to the app, reload it with `docker-compose build web && docker-compose up -d web`.
- Follow logs with `docker-compose logs -f`.

**Note**: The SQL Server image requires 4G of RAM. You'll probably have to increase this limit in Docker settings.

### Docker on Windows

Docker for Windows runs linux containers by default, and this works well.  "Windows Containers" would be more performant, but will will not work out-of-the-box.

We include a `.env.example.ps1` for Powershell.  To seed your database change the extension of `seed-local-databases.sh` to `.ps1` and run it in Powershell.

## Running Tests

```bash
cd AKRestAPI.Tests
dotnet test
```