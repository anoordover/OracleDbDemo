namespace ExecuteUpdateDemo.Data;

public class Credit
{
    public long Id { get; set; }

    public Guid GuidField { get; set; }

    public string Reference { get; set; }

    public string DeclarationReference { get; set; }
    
    public Period Period { get; set; }
    
    public long? DeclarationId { get; set; }

    public Declaration? Declaration { get; set; }
    
    public OptionalPeriod OptionalPeriod { get; set; }
}

public class OptionalPeriod
{
    public DateTime? DatumVanaf { get; set; }
    public DateTime? DatumTm { get; set; }
}

public class Period
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}