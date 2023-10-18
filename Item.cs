using System;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ShelfId { get; set; }
    public string Type { get; set; }
    public string Kashrut { get; set; }
    public DateTime ExpiryDate { get; set; }
    public double SpaceTaken { get; set; }

    public Item(string itemId, string name, string type, string kosherType, DateTime expiryDate, double spaceTaken)
    {
        Id = itemId;
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