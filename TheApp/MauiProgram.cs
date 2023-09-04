using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheApp.Data;

namespace TheApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddTransient<IDataService, DataService>();

            var migrationsAssembly = typeof(AppDbContext).Assembly.GetName().Name;
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");
            builder.Services.AddDbContext<AppDbContext>(b => b.UseSqlite($"Filename={dbPath}.db", x => x.MigrationsAssembly(migrationsAssembly)));

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
