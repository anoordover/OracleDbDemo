using ExecuteUpdateDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExecuteUpdateDemo;

public class Program
{
    public static void Main(string[] args)
    {
        var sp = new ServiceCollection()
            .AddDbContext<DemoDbContext>(options =>
            {
                options.UseOracle(
                    "Data Source=dev;Persist Security Info=True;User ID=demo;Password=demo;");
                options.LogTo(Console.WriteLine);
            })
            .BuildServiceProvider();

        using var db = sp.GetRequiredService<DemoDbContext>();
        {
            var credits = db.Credits.ToList();

            Console.WriteLine(credits.Count);
            
            ExecuteUpdateWithJoin(db);
            PrintSeparator();
            ExecuteUpdateWithSelectNew1(db);
            PrintSeparator();
            ExecuteUpdateWithSelectNew2(db);
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
    private static void ExecuteUpdateWithSelectNew2(DemoDbContext db)
    {
        try
        {
            var r = db.Contestations.Where(c => c.Id == 1)
                .Select(c => new
                {
                    contestation = c,
                    credit = db.Credits
                        .First(d => d.Reference == c.CreditReference)
                })
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.contestation.CreditId,
                    c => c.credit.Id));
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
}