using DotNetCapTest.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCapTest.Web
{
    public static class DatabaseSetupExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}
