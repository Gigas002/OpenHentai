# Migration script and info
# requires:
# - dotnet >= 7.0.5 and <= 8.x
# - dotnet-ef (dotnet tool install --global dotnet-ef)
# - PostgreSQL (later)
# - reference to Microsoft.EntityFrameworkCore.Design (dotnet add package Microsoft.EntityFrameworkCore.Design)

migrationName=$1
dotnet ef migrations add $migrationName
dotnet ef migrations script > script.sql
dotnet ef database update
