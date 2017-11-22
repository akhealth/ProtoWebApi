# ASP.NET Core vars
$env:ASPNETCORE_ENVIRONMENT="Development"

# SOAP endpoint
$env:SoapEndpoint=""

# SQL Server connection
$env:SqlConnectionString="Server=sqlserver,1433;Database=AKTestDataBase;User ID=SA;Password=BaldEagle123"
$env:SqlQuery="SELECT * FROM people;"

# Postgres connection
$env:PgConnectionString="Server=postgres;User Id=postgres;Database=18FDatabase"

# EIS Web Service, note the trailing slash
$env:EISEndpoint=""