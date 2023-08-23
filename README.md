#Product

dotnet ef migrations add "Migrationname" --project .\Product\src\Infastructure --startup-project .\Product\src\WebAPI\WebAPI.csproj --output-dir Migrations --context ApplicationDbContext

dotnet ef database update --project .\Product\src\Infastructure --startup-project .\Product\src\WebAPI\WebAPI.csproj --context ApplicationDbContext

update-database -Context ApplicationDbContext


#Order

dotnet ef migrations add "Initial" --project .\Order\src\Infastructure --startup-project .\Order\src\WebAPI\WebAPI.csproj --output-dir Migrations --context ApplicationDbContext

dotnet ef database update --project .\Order\src\Infastructure --startup-project .\Order\src\WebAPI\WebAPI.csproj --context ApplicationDbContext