// See https://aka.ms/new-console-template for more information
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

Console.WriteLine("dotnet ef migrations add <NAME> --project ./database/database.csproj --startup-project ./databaseEntryPoint/DatabaseEntryPoint.csproj");

public class DesignDbContext : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("NO").Options);
    }
}