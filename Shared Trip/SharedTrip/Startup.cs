namespace SharedTrip
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using SharedTrip.Common;
    using SharedTrip.Contracts;
    using SharedTrip.Data;
    using SharedTrip.Services;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection
                .Add<ApplicationDbContext>()
                .Add<IRepository, Repository>()
                .Add<IUserService, UserService>()
                .Add<ITripService, TripService>()
                .Add<IValidationService, ValidationService>();

            await server.Start();
        }
    }
}
