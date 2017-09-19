# ASP.NET Core vars
export ASPNETCORE_ENVIRONMENT="Development"

# SOAP endpoint
export SoapEndpoint="https://esbtest.dhss.alaska.gov/MCIPersonService/PersonService.svc"
export SoapHttpBasicUser=""
export SoapHttpBasicPass=""

# SQL Server connection
export SqlConnectionString="Server=sqlserver,1433;Database=AKTestDataBase;User ID=SA;Password=BaldEagle123"
export SqlQuery="SELECT * FROM people;"

# Postgres connection
export PgConnectionString="Server=postgres;User Id=postgres;Database=18FDatabase"
export PgQuery="SELECT * FROM People LIMIT 42;"