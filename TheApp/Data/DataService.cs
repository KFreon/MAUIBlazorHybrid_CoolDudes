using Database;
using Microsoft.EntityFrameworkCore;

namespace TheApp.Data;

public interface IDataService
{
    Task AddADude(string name, bool isCool);
    Task DeleteADude(CoolDude dude);
    Task<CoolDude[]> GetDudes();
    Task Initialise();
}

public class DataService : IDataService
{
    private readonly AppDbContext dbContext;
    private static bool Initialised = false;

    public DataService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Initialise()
    {
        if (!Initialised)
        {
            await dbContext.Database.MigrateAsync();
            Initialised = true;
        }
    }

    public async Task AddADude(string name, bool isCool)
    {
        dbContext.Add(new CoolDude(name, isCool));
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteADude(CoolDude dude)
    {
        dbContext.Remove(dude);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CoolDude[]> GetDudes()
    {
        return await dbContext.CoolDudes.ToArrayAsync();
    }
}