namespace ExecuteUpdateDemo.Data;

public class Contestation
{
    public long Id { get; set; }

    public string Reference { get; set; }

    public string DeclarationReference { get; set; }

    public string CreditReference { get; set; }

    public long? CreditId { get; set; }

    public Credit? Credit { get; set; }

    public long? DeclarationId { get; set; }

    public Declaration? Declaration { get; set; }
}