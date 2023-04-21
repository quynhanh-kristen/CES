//using System.Security.Policy;
using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Client.Web;

public static class DatabaseInitialization
{
    public static async Task SeedDatabase(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<RoutePlanningDatabaseContext>();
        await context.Database.EnsureCreatedAsync();

        var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        await using (var unitOfWork = unitOfWorkManager.Initiate())
        {
            await SeedUsers(context);
            await SeedLocationsAndRoutes(context);

            unitOfWork.Commit();
        }
    }

    private static async Task SeedLocationsAndRoutes(RoutePlanningDatabaseContext context)
    {

        var tanger = new Location("Tanger");
        await context.AddAsync(tanger);

        var kanariskeOer = new Location("De Kanariske oer");
        await context.AddAsync(kanariskeOer);

        var marrakesh = new Location("Marrakesh");
        await context.AddAsync(marrakesh);

        var dakar = new Location("Dakar");
        await context.AddAsync(dakar);


        var sierraLeone = new Location("Sierra Leone");
        await context.AddAsync(sierraLeone);

        var guldkysten = new Location("Guldkysten");
        await context.AddAsync(guldkysten);


        var stHelena = new Location("St. Helena");
        await context.AddAsync(stHelena);

        var tunis = new Location("Tunis");
        await context.AddAsync(tunis);

        var sahara = new Location("Sahara");
        await context.AddAsync(sahara);

        var timbuktu = new Location("Timbuktu");
        await context.AddAsync(timbuktu);

        var tripoli = new Location("Tripoli");
        await context.AddAsync(tripoli);

        var cairo = new Location("Cairo");
        await context.AddAsync(cairo);

        var wadai = new Location("Wadai");
        await context.AddAsync(wadai);

        var omdurman = new Location("Omdurman");
        await context.AddAsync(omdurman);

        var darfur = new Location("Darfur");
        await context.AddAsync(darfur);

        var slavekysten = new Location("Slavekysten");
        await context.AddAsync(slavekysten);

        var congo = new Location("Congo");
        await context.AddAsync(congo);

        var luanda = new Location("Luanda");
        await context.AddAsync(luanda);

        var hvalbugten = new Location("Hvalbugten");
        await context.AddAsync(hvalbugten);

        var kapstaden = new Location("Kapstaden");
        await context.AddAsync(kapstaden);

        var kabald = new Location("Kabald");
        await context.AddAsync(kabald);

        var bharelGhazal = new Location("Bharel Ghazal");
        await context.AddAsync(bharelGhazal);

        var suakin = new Location("Suakin");
        await context.AddAsync(suakin);

        var addisAbeba = new Location("Addis Abeba");
        await context.AddAsync(addisAbeba);

        var kapGuardafui = new Location("Kap Guardafui");
        await context.AddAsync(kapGuardafui);

        var victoriasoen = new Location("Victoriasoen");
        await context.AddAsync(victoriasoen);

        var zanzibar = new Location("Zanzibar");
        await context.AddAsync(zanzibar);

        var mocambique = new Location("Mocambique");
        await context.AddAsync(mocambique);

        var victoriafalden = new Location("Victoriafalden");
        await context.AddAsync(victoriafalden);

        var dragebjerget = new Location("Dragebjerget");
        await context.AddAsync(dragebjerget);

        var kapStMarie = new Location("Kap St. Marie");
        await context.AddAsync(kapStMarie);

        var amatave = new Location("Amatave");
        await context.AddAsync(amatave);

        CreateTwoWayConnection(tanger, marrakesh, 8, 0, 1);
        CreateTwoWayConnection(tanger, tunis, 20, 0, 1);
        CreateTwoWayConnection(tunis, tripoli, 12, 0, 1);
        CreateTwoWayConnection(tripoli, omdurman, 24, 0, 1);
        CreateTwoWayConnection(omdurman, cairo, 16, 0, 1);
        CreateTwoWayConnection(tanger, sahara, 20, 0, 1);
        CreateTwoWayConnection(sahara, darfur, 32, 0, 1);
        CreateTwoWayConnection(marrakesh, dakar, 32, 0, 1);
        CreateTwoWayConnection(dakar, sierraLeone, 16, 0, 1);
        CreateTwoWayConnection(sierraLeone, timbuktu, 20, 0, 1);
        CreateTwoWayConnection(timbuktu, guldkysten, 16, 0, 1);
        CreateTwoWayConnection(sierraLeone, guldkysten, 20, 0, 1);
        CreateTwoWayConnection(timbuktu, slavekysten, 20, 0, 1);
        CreateTwoWayConnection(slavekysten, wadai, 28, 0, 1);
        CreateTwoWayConnection(wadai, darfur, 16, 0, 1);
        CreateTwoWayConnection(darfur, congo, 24, 0, 1);
        CreateTwoWayConnection(slavekysten, darfur, 28, 0, 1);
        CreateTwoWayConnection(slavekysten, congo, 20, 0, 1);
        CreateTwoWayConnection(congo, luanda, 12, 0, 1);
        CreateTwoWayConnection(luanda, kabald, 16, 0, 1);
        CreateTwoWayConnection(luanda, mocambique, 40, 0, 1);
        CreateTwoWayConnection(zanzibar, mocambique, 12, 0, 1);
        CreateTwoWayConnection(victoriasoen, mocambique, 24, 0, 1);
        CreateTwoWayConnection(victoriasoen, zanzibar, 20, 0, 1);
        CreateTwoWayConnection(kabald, victoriasoen, 16, 0, 1);
        CreateTwoWayConnection(victoriafalden, mocambique, 20, 0, 1);
        CreateTwoWayConnection(dragebjerget, mocambique, 16, 0, 1);
        CreateTwoWayConnection(dragebjerget, victoriafalden, 12, 0, 1);
        CreateTwoWayConnection(hvalbugten, kapstaden, 16, 0, 1);
        CreateTwoWayConnection(victoriafalden, hvalbugten, 16, 0, 1);
        CreateTwoWayConnection(zanzibar, kapGuardafui, 24, 0, 1);
        CreateTwoWayConnection(addisAbeba, kapGuardafui, 12, 0, 1);
        CreateTwoWayConnection(addisAbeba, victoriasoen, 12, 0, 1);
        CreateTwoWayConnection(suakin, addisAbeba, 12, 0, 1);
        CreateTwoWayConnection(darfur, suakin, 16, 0, 1);
        CreateTwoWayConnection(darfur, bharelGhazal, 8, 0, 1);
        CreateTwoWayConnection(bharelGhazal, victoriasoen, 8, 0, 1);
        CreateTwoWayConnection(omdurman, darfur, 12, 0, 1);

        CreateTwoWayConnection(tanger, sierraLeone, 8, 0, 2);
        CreateTwoWayConnection(sierraLeone, stHelena, 8, 0, 2);
        CreateTwoWayConnection(kapstaden, stHelena, 8, 0, 2);
        CreateTwoWayConnection(kapstaden, kapStMarie, 8, 0, 2);
        CreateTwoWayConnection(kapstaden, amatave, 8, 0, 2);
        CreateTwoWayConnection(kapstaden, dragebjerget, 8, 0, 2);
        CreateTwoWayConnection(kapstaden, hvalbugten, 8, 0, 2);
        CreateTwoWayConnection(guldkysten, hvalbugten, 8, 0, 2);
        CreateTwoWayConnection(guldkysten, luanda, 8, 0, 2);
        CreateTwoWayConnection(kabald, darfur, 8, 0, 2);
        CreateTwoWayConnection(guldkysten, tripoli, 8, 0, 2);
        CreateTwoWayConnection(tanger, tripoli, 8, 0, 2);
        CreateTwoWayConnection(tripoli, darfur, 8, 0, 2);
        CreateTwoWayConnection(suakin, darfur, 8, 0, 2);
        CreateTwoWayConnection(suakin, cairo, 8, 0, 2);
        CreateTwoWayConnection(suakin, victoriasoen, 8, 0, 2);
        CreateTwoWayConnection(kapGuardafui, victoriasoen, 8, 0, 2);
        CreateTwoWayConnection(kapGuardafui, amatave, 8, 0, 2);
        CreateTwoWayConnection(victoriasoen, mocambique, 8, 0, 2);

       //ship list
        CreateTwoWayConnection(tanger, kanariskeOer, 36, 0, 0);
        CreateTwoWayConnection(dakar, kanariskeOer, 60, 0, 0);
        CreateTwoWayConnection(dakar, sierraLeone, 36, 0, 0);
        CreateTwoWayConnection(dakar, stHelena, 120, 0, 0);
        CreateTwoWayConnection(sierraLeone, guldkysten, 48, 0, 0);
        CreateTwoWayConnection(guldkysten, slavekysten, 48, 0, 0);
        CreateTwoWayConnection(slavekysten, hvalbugten, 108, 0, 0);
        CreateTwoWayConnection(hvalbugten, kapstaden, 36, 0, 0);
        CreateTwoWayConnection(stHelena, kapstaden, 108, 0, 0);
        CreateTwoWayConnection(stHelena, hvalbugten, 108, 0, 0);
        CreateTwoWayConnection(kapstaden, kapStMarie, 96, 0, 0);
        CreateTwoWayConnection(kapStMarie, mocambique, 36, 0, 0);
        CreateTwoWayConnection(kapGuardafui, amatave, 96, 0, 0);
        CreateTwoWayConnection(mocambique, kapGuardafui, 96, 0, 0);
        CreateTwoWayConnection(cairo, suakin, 48, 0, 0);
        CreateTwoWayConnection(suakin, kapGuardafui, 48, 0, 0);
    }

    private static async Task SeedUsers(RoutePlanningDatabaseContext context)
    {
        var alice = new User("alice", User.ComputePasswordHash("alice123!"));
        await context.AddAsync(alice);

        var bob = new User("bob", User.ComputePasswordHash("!CapableStudentCries25"));
        await context.AddAsync(bob);
    }

    private static void CreateTwoWayConnection(Location locationA, Location locationB, int distance, int price, int typeOfTransport)
    {
        locationA.AddConnection(locationB, distance, price, typeOfTransport);
        locationB.AddConnection(locationA, distance, price, typeOfTransport);
    }
}
