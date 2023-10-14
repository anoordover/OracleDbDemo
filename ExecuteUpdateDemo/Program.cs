using ExecuteUpdateDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.Oracle;

namespace ExecuteUpdateDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        var container = new OracleBuilder()
            .Build();
        await container.StartAsync();

        var sp = new ServiceCollection()
            .AddDbContext<DemoDbContext>(options =>
            {
                options.UseOracle(
                    container.GetConnectionString());
                options.LogTo(Console.WriteLine);
            })
            .BuildServiceProvider();

        await using var db = sp.GetRequiredService<DemoDbContext>();
        {
            await db.Database.MigrateAsync();
            var credits = db.Credits.ToList();

            Console.WriteLine(credits.Count);
            
            ExecuteUpdateWithJoin(db);
            Console.ReadLine();
            PrintSeparator();
            ExecuteUpdateWithJoin2(db);
            Console.ReadLine();
            PrintSeparator();
            ExecuteUpdateWithSelectNew1(db);
            Console.ReadLine();
            PrintSeparator();
            ExecuteUpdateWithSelectNew2(db);
            Console.ReadLine();
            PrintSeparator();
            ExecuteUpdateWithSelectNew3(db);
            Console.ReadLine();
        }

    }

    private static void PrintSeparator()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("===========================================");
        Console.WriteLine("===========================================");
    }

    private static void ExecuteUpdateWithSelectNew1(DemoDbContext db)
    {
        try
        {
            var r = db.Credits.Where(c => c.Id == 1)
                .Select(c => new
                {
                    credit = c,
                    declaration = db.Declarations
                        .First(d => d.Reference == c.DeclarationReference)
                })
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.credit.DeclarationId,
                    c => c.declaration.Id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    private static void ExecuteUpdateWithSelectNew3(DemoDbContext db)
    {
        try
        {
            var r = db.Credits.Where(c => c.Id == 1)
                .Select(c => new
                {
                    credit = new Credit
                    {
                        Id = c.Id,
                        DeclarationId = c.DeclarationId
                    },
                    declaration = db.Declarations
                        .First(d => d.Reference == c.DeclarationReference)
                })
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.credit.DeclarationId,
                    c => c.declaration.Id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    private static void ExecuteUpdateWithSelectNew2(DemoDbContext db)
    {
        try
        {
            var r = db.Contestations.Where(c => c.Id == 1)
                .Select(c => new
                {
                    contestation = c,
                    credit = db.Credits
                        .First(d => d.Reference == c.CreditReference).Id
                })
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.contestation.CreditId,
                    c => c.credit));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private static void ExecuteUpdateWithJoin(DemoDbContext db)
    {
        try
        {
            var s = db.Credits.Where(c => c.Id == 1)
                .Join(db.Declarations,
                    c => c.DeclarationReference,
                    d => d.Reference,
                    (credit, declaration) => new {credit, declaration})
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.credit.DeclarationId,
                    c => c.declaration.Id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private static void ExecuteUpdateWithJoin2(DemoDbContext db)
    {
        try
        {
            var s = db.Contestations.Where(c => c.Id == 1)
                .Join(db.Credits,
                    c => c.DeclarationReference,
                    d => d.Reference,
                    (contestation, credit) => new {contestation, credit.Id})
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.contestation.CreditId,
                    c => c.Id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}