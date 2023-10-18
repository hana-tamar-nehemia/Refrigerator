using System;

public class Item
{
    private static int counter = 1;
    public string Id { get; }
    public string Name { get; set; }
    public string ShelfId { get; set; }
    public string Type { get; set; }
    public string Kashrut { get; set; }
    public DateTime ExpiryDate { get; set; }
    public double SpaceTaken { get; set; }

    public Item( string name, string type, string kosherType, DateTime expiryDate, double spaceTaken)
    {
        Id = counter.ToString();
        counter++;
        Name = name;
        Type = type;
        Kashrut = kosherType;
        ExpiryDate = expiryDate;
        SpaceTaken = spaceTaken;
    }

    public override string ToString()
    {
        return $"Item ID: {Id}, Name: {Name}, Shelf ID: {ShelfId}, Type: {Type}, Kashrut: {Kashrut}, Expiry Date: {ExpiryDate}, Space Taken: {SpaceTaken}cm^2";
    }
}