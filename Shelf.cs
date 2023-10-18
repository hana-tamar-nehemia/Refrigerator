using System;
using System.Collections.Generic;

public class Shelf
{
    public string Id { get; set; }
    public int Floor { get; set; }
    public double MaxSpace { get; set; }
    public List<Item> Items { get; set; }

    public Shelf(string id, int floor, double MaxSpace)
    {
        Id = id;
        Floor = floor;
        MaxSpace = MaxSpace;
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
        return (MaxSpace - totalItemSpace);
    }
    public string ToString()
    {
        return $"Shelf ID: {Id}, Floor: {Floor}, Max Space: {MaxSpace}cm^2";
    }
}


