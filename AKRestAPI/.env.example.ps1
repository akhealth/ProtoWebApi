# ASP.NET Core vars
$env:ASPNETCORE_ENVIRONMENT="Development"

# SOAP endpoint
$env:SoapEndpoint="https://esbtest.dhss.alaska.gov/MCIPersonService/PersonService.svc"

# SQL Server connection
$env:SqlConnectionString="Server=sqlserver,1433;Database=AKTestDataBase;User ID=SA;Password=BaldEagle123"
$env:SqlQuery="SELECT * FROM people;"

# Postgres connection
$env:PgConnectionString="Server=postgres;User Id=postgres;Database=18FDatabase"
$env:PgQuery="SELECT * FROM People LIMIT 42;"