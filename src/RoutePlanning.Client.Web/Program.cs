using Netcompany.Net.Cqs;
using Netcompany.Net.ErrorHandling;
using Netcompany.Net.Logging.Serilog;
using Netcompany.Net.UnitOfWork;
using Netcompany.Net.Validation;
using RoutePlanning.Application;
using RoutePlanning.Infrastructure;

namespace RoutePlanning.Client.Web;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRoutePlanningInfrastructure();
        builder.Services.AddRoutePlanningApplication();

        builder.Services.AddCqs(options => options.UseValidation().UseUnitOfWork());
        builder.Services.AddUnhandledExceptionMiddleware();
        builder.Services.AddValidationMiddleware();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSimpleAuthentication();
        builder.Services.AddApiTokenAuthorization(builder.Configuration);

        builder.Host.ConfigureLoggingDefaults();

        var app = builder.Build();

        await DatabaseInitialization.SeedDatabase(app);

        if (app.Environment.IsProduction())
        {
            app.UseUnhandledExceptionMiddleware();
        }

        app.UseValidationMiddleware();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
