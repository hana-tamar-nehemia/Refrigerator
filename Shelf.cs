using System;
using System.Collections.Generic;

public class Shelf
{
    private static int counter = 1;
    public string Id { get; }
    public int Floor { get; set; }
    public double Space { get; set; }
    public List<Item> Items { get; set; }

    public Shelf(int floor, double MaxSpace)
    {
        Id = counter.ToString();
        counter++;
        Floor = floor;
        Space = MaxSpace;
        Items = new List<Item>();
    }
    public double SpaceTakenInShelf()
    {
        double totalSpace = 0;
        foreach (var item in Items)
        {
            totalSpace += item.SpaceTaken;
        }
        return totalSpace;
    }
    public double AvailableSpaceInShelf()
    {
        double totalItemSpace = 0;
        foreach (var item in Items)
        {
            totalItemSpace += item.SpaceTaken;
        }
        return (Space - totalItemSpace);
    }
    public string ToString()
    {
        return $"Shelf ID: {Id}, Floor: {Floor}, Space: {Space}cm^2";
    }
}


