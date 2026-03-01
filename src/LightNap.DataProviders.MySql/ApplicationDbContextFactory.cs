using LightNap.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LightNap.DataProviders.MySql
{
    /// <summary>
    /// Design-time factory used by EF Core tools (dotnet ef migrations) to create
    /// an ApplicationDbContext configured for MySQL without needing the full WebApi startup.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySQL(
                "Server=localhost;Database=GastroManagementDB;User=root;Password=password;",
                options => options.MigrationsAssembly("LightNap.DataProviders.MySql"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
