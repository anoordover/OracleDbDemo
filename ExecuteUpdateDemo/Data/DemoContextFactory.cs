using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Testcontainers.Oracle;

namespace ExecuteUpdateDemo.Data;

public class DemoContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {
        var container = new OracleBuilder()
            .Build();
        Task.Run(() => container.StartAsync()).Wait();
        var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
        optionsBuilder.UseOracle(container.GetConnectionString());

        return new DemoDbContext(optionsBuilder.Options);
    }
}
