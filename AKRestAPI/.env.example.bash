# ASP.NET Core vars
ASPNETCORE_ENVIRONMENT="Development"

# SOAP endpoint
SoapEndpoint="https://esbtest.dhss.alaska.gov/MCIPersonService/PersonService.svc"

# SQL Server connection
SqlConnectionString="Server=sqlserver,1433;Database=AKTestDataBase;User ID=SA;Password=BaldEagle123"
SqlQuery="SELECT * FROM people;"

# Postgres connection
PgConnectionString="Server=postgres;User Id=postgres;Database=18FDatabase"
PgQuery="SELECT * FROM People LIMIT 42;"