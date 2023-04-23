using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExecuteUpdateDemo.Data;

public class DemoContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
        optionsBuilder.UseOracle("Data Source=dev;Persist Security Info=True;User ID=demo;Password=demo;");

        return new DemoDbContext(optionsBuilder.Options);
    }
}
