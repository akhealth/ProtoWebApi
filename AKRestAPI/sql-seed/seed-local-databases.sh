
# This `sql-seed` folder gets mounted in containers so we can run these .sql scripts

# Seed Postgres
docker-compose exec postgres createdb -U postgres 18FDatabase
docker-compose exec postgres psql -d 18FDatabase -U postgres -f /sql-seed/postgres.sql

# Seed SQL Server
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'BaldEagle123' -i /sql-seed/sqlserver.sql