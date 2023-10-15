using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Testcontainers.PostgreSql;

namespace ExecuteUpdateDemo.Data;

public class DemoContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {
        var container = new PostgreSqlBuilder()
            .Build();
        Task.Run(() => container.StartAsync()).Wait();
        var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
        optionsBuilder.UseNpgsql(container.GetConnectionString());

        return new DemoDbContext(optionsBuilder.Options);
    }
}
