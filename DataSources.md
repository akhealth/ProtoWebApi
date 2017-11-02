# Data Sources

This prototype search app makes use of a couple different Alaska data sources:

0. MCI, a SOAP service
1. ARIES Test, a Postgres database via direct connection
2. EIS Test, mainframe data exposed via a web service.

More details on each below.

## MCI

- Master Client Index
- *Available over VPN*
- On-prem SOAP Service
- our API wrapper: http://localhost:5000/mci/people/findByName?firstName=Greg&lastName=Allen
- http://localhost:5000/mci/people/findByName?firstName=Johana&lastName=Leboeuf


## ARIES Test data

- Postgres database
- *NOT available over VPN*
- When this prototype is running in Azure you can leverage the Hybrid Connection
- Running in staging: https://protowebapi-staging.azurewebsites.net/aries?ids=2400127130,2400141779

The ARIES IDs come from the MCI

## EIS Test data

- Actually in the Mainframe
- Exposed by an on-prem HTTP-based web service
- *Available over VPN* but had a certificate warning
- https://hss18fpoc-test.soa.alaska.gov/AE_Interface/hes18f01svc/1/0600093208
- Our API wrapper: http://localhost:5000/eis?id=0600093208

The `0600` number is the only parameter in that request

## LEGACY SQL Server
http://localhost:5000/sql connects to MS SQL Server. This originates from testing Connx Linked Server.

# A Data Story

1.
    - http://localhost:5000/mci/people/findByName?firstName=Johana&lastName=Leboeuf has ARIES ID 2400000003
    - https://protowebapi-staging.azurewebsites.net/aries?ids=2400000003
    - We lose this story in EIS

TODO: we need to find some data that exists across all of these test systems!

# AK self-signed certs

Currently we access EIS Webservice over a self-signed SSL Cert, which depends on an internal CA Cert.
This is a little problematic for development:

1. Untrusted requests fail by default in .Net.  Configuring the prototype to just ignore certificate warnings _only_ works in Windows environments because .Net has problems driving the underlying libs like `openssl` in OSX/Linux.
2. We can add the AK CA Cert to our OSX development system keychain, which doesn't satisfy direct-chrome-browser-requests, but does satisfy the .Net runtime.
3. I'm not yet sure if we can add a custom CA Cert into our Azure AppService/Hybrid environment, am talking to Azure support about this.

## How to handle AK CA Certs for now

1. Local Dev: Add the custom CA Cert to your keychain.  Comment out the `handler.ServerCertificateCustomValidationCallback` line in `EISController.cs`
2. Azure: Until we know if we can add a custom CA Cert, we need to make sure the handler ^^ is present in code so we can bypass the certificate error there.
