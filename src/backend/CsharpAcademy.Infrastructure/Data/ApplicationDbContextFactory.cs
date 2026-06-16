using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CsharpAcademy.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=csharpacademy;Uid=root;Pwd=;CharSet=utf8mb4;",
            new MySqlServerVersion(new Version(8, 0, 0)));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
