namespace ExecuteUpdateDemo.Data;

public class Credit
{
    public long Id { get; set; }

    public string Reference { get; set; }

    public string DeclarationReference { get; set; }
    
    public long? DeclarationId { get; set; }

    public Declaration? Declaration { get; set; }
}