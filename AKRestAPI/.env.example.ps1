# ASP.NET Core vars
$env:ASPNETCORE_ENVIRONMENT="Development"

# SOAP endpoint
$env:SoapEndpoint="https://esbtest.dhss.alaska.gov/MCIPersonService/PersonService.svc"

# SQL Server connection
$env:SqlConnectionString="Server=sqlserver,1433;Database=AKTestDataBase;User ID=SA;Password=BaldEagle123"
$env:SqlQuery="SELECT * FROM people;"

# Postgres connection
$env:PgConnectionString="Server=postgres;User Id=postgres;Database=18FDatabase"

# EIS Web Service, note the trailing slash
$env:EISEndpoint="https://hss18fpoc-test.soa.alaska.gov/AE_Interface/hes18f01svc/1/"