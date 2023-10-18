using System;
using System.Collections.Generic;

public class Refrigerator
{
    public string Id { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int NumShelves { get; set; }
    public List<Shelf> Shelves { get; set; }


    public Refrigerator(string id, string model, string color, int numberOfShelves, List<Shelf> shelves)
    {
        Id = id;
        Model = model;
        Color = color;
        NumShelves = numberOfShelves;
        Shelves = shelves;
    }

    //לוקחים את סך כל המקום הפנוי שיש במדפים
    public double AvailableSpaceOnRefrigerator()
    {
        double totalSpace = 0;
        foreach (var shelf in Shelves)
        {
            totalSpace += shelf.AvailableSpaceInShelf(); ;
        }
        return totalSpace;
    }

    //כל פריט שהתאריך שלו קטן מהתאריך הנוכחי יצא מהמדף וככה בכל מדף
    public void CleanExpiryDate()
    {
        foreach (var shelf in Shelves)
        {
            shelf.Items.RemoveAll(item => item.ExpiryDate < DateTime.Now);
        }
    }

    //עוברים על כל הפריטים במקרר ומכניסים לרשימת החזרה רק
    //מי שלפי הכשרות הרצויה והסוג וגם אם זה לא פג תוקף
    public List<Item> GetItemsByKashrutAndType(string kashrut, string type)
    {
        List<Item> items = new List<Item>();
        foreach (var shelf in Shelves)
        {
            foreach (var item in shelf.Items)
            {
                if (item.Kashrut == kashrut && item.Type == type && item.ExpiryDate >= DateTime.Now)
                {
                    items.Add(item);
                }
            }
        }
        return items;
    }

    //עוברים מדף מדף וכשמגיעים למדף שיש בו מקום מכניסים אליו את הפריט
    public void AddItem(Item item)
    {
        foreach (var shelf in Shelves)
        {
            if (shelf.SpaceTakenInShelf() >= item.SpaceTaken)
            {
                item.ShelfId = shelf.Id;
                shelf.Items.Add(item);
            }
        }
    }

    //עוברים מדף מדף ומוצאים את הפריט אם מצאנו אותו
    public Item RemoveItem(string itemId)
    {
        foreach (var shelf in Shelves)
        {
            foreach (var item in shelf.Items)
            {
                if (item.Id == itemId)
                {
                    shelf.Items.Remove(item);
                    return item;
                }
            }
        }
        return null;
    }
    public List<Item> allItems()
    {

        List<Item> allItems = new List<Item>();
        foreach (var shelf in Shelves)
        {
            allItems.AddRange(shelf.Items);
        }
        return allItems;
    }


    //ניצור רשימה חדשה של כל הפריטים שיש במקרר ואז נמיין אותה
    public List<Item> SortByExpiryDate()
    {
        List<Item> allItems = new List<Item>();
        foreach (var shelf in Shelves)
        {
            allItems.AddRange(shelf.Items);
        }
        allItems.Sort((item1, item2) => item1.ExpiryDate.CompareTo(item2.ExpiryDate));
        return allItems;
    }

    //נמיין מדפים עי השוואה בין גודל המקום הפנוי במדף
    public List<Shelf> SortShelvesBySpace()
    {
        Shelves.Sort((shelf1, shelf2) => shelf2.AvailableSpaceInShelf().CompareTo(shelf1.AvailableSpaceInShelf()));
        return Shelves;
    }

    //נמיין מדפים עי השוואה בין גודל המקום הפנוי במקררים
    public List<Refrigerator> SortRefrigeratorsBySpace()
    {
        List<Refrigerator> refrigerators = new List<Refrigerator> { this };
        refrigerators.Sort((refrigerator1, refrigerator2) => refrigerator2.AvailableSpaceOnRefrigerator().CompareTo(refrigerator1.AvailableSpaceOnRefrigerator()));
        return refrigerators;
    }

    public override string ToString()
    {
        return $"Refrigerator ID: {Id}, Model: {Model}, Color: {Color}, Number of Shelves: {NumShelves}, Available Space: {AvailableSpaceOnRefrigerator()}cm^2";
    }

    public void CleanForShoping()
    {

        if (AvailableSpaceOnRefrigerator() >= 29)
        {
            Console.WriteLine("There is space");
            return;
        }

        CleanExpiryDate();

        if (AvailableSpaceOnRefrigerator() >= 29)
        {
            Console.WriteLine("There is space");
            return;
        }

        List<Item> allItems = new List<Item>();
        List<Item> toDiscard = new List<Item>();

        foreach (var shelf in Shelves)
        {
            allItems.AddRange(shelf.Items);
        }

        foreach (var item in allItems)
        {
            if (item.Kashrut == "Dairy" && (item.ExpiryDate - DateTime.Now).Days < 3)
            {
                toDiscard.Add(item);
                Console.WriteLine($"Item {item.Name} with ID {item.Id} has been discarded.");
                RemoveItem(item.Id);

            }
        }
        if (AvailableSpaceOnRefrigerator() < 29)
        {
            foreach (var item in allItems)
            {
                if (item.Kashrut == "Meat" && (item.ExpiryDate - DateTime.Now).Days < 7)
                {
                    toDiscard.Add(item);
                    Console.WriteLine($"Item {item.Name} with ID {item.Id} has been discarded.");
                    RemoveItem(item.Id);
                }
            }
        }
        else
        {
            Console.WriteLine("There is space");
            return;
        }

        if (AvailableSpaceOnRefrigerator() < 29)
        {
            foreach (var item in allItems)
            {
                if (item.Kashrut == "Pareve" && (item.ExpiryDate - DateTime.Now).Days < 1)
                {
                    toDiscard.Add(item);
                    Console.WriteLine($"Item {item.Name} with ID {item.Id} has been discarded.");
                    RemoveItem(item.Id);
                }
            }
        }
        else
        {
            Console.WriteLine("There is space");
            return;
        }


        if (AvailableSpaceOnRefrigerator() >= 29)
        {
            Console.WriteLine("there is enough space in the refrigerator for new shoping.");
        }
        else
        {
            allItems.AddRange(toDiscard);
            Console.WriteLine("There is still not enough space in the refrigerator so The expired products have been returned to the fridge, you will shop another day. .");
        }
    }

}
