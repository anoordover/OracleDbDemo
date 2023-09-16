# OracleDbDemo for ExecuteUpdate findings

## Using join

```csharp
            var s = db.Credits.Where(c => c.Id == 1)
                .Join(db.Declarations,
                    c => c.DeclarationReference,
                    d => d.Reference,
                    (credit, declaration) => new {credit, declaration})
                .ExecuteUpdate(calls => calls.SetProperty(
                    c => c.credit.DeclarationId,
                    c => c.declaration.Id));
```

The SQL being generated is incorrect
```sql
      UPDATE "Credits" "c"
      SET "c"."DeclarationId" = "d"."Id"
      FROM "Declarations" "d"
      WHERE (("c"."DeclarationReference" = "d"."Reference") AND ("c"."Id" = 1))
```

## Using select

```csharp
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
```

This code generates correct SQL
```sql
      UPDATE "Credits" "c"
      SET "c"."DeclarationId" = (
          SELECT "d"."Id"
          FROM "Declarations" "d"
          WHERE "d"."Reference" = "c"."DeclarationReference"
          FETCH FIRST 1 ROWS ONLY)
      WHERE "c"."Id" = 1
```

## Using select with two entities/tables starting with same letter

**This issue is solved in version 7.0.11 of EF Core.**

```csharp
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
```

In this sql-statement the same alias is being reused
```sql
      UPDATE "Contestations" "c"
      SET "c"."CreditId" = (
          SELECT "c"."Id"
          FROM "Credits" "c"
          WHERE "c"."Reference" = "c"."CreditReference"
          FETCH FIRST 1 ROWS ONLY)
      WHERE "c"."Id" = 1
```

This issue is solved in 7.0.11. The sql-statement being generated is
```sql
      UPDATE "Contestations" "c"
      SET "c"."CreditId" = (
          SELECT "c0"."Id"
          FROM "Credits" "c0"
          WHERE "c0"."Reference" = "c"."CreditReference"
          FETCH FIRST 1 ROWS ONLY)
      WHERE "c"."Id" = 1
```