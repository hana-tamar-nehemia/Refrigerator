using System;
using System.Collections.Generic;

public class Refrigerator
{
    private static int counter = 1;
    public string Id { get; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int NumShelves { get; set; }
    public List<Shelf> Shelves { get; set; }


    public Refrigerator( string model, string color, int numberOfShelves, List<Shelf> shelves)
    {
        Id = counter.ToString();
        counter++;
        Model = model;
        Color = color;
        NumShelves = numberOfShelves;
        Shelves = shelves;
    }

    //פונקציה שמחזירה כמה מקום נשאר במקרר.
    public double AvailableSpaceOnRefrigerator()
    {
        double totalSpace = 0;
        foreach (var shelf in Shelves)
        {
            totalSpace += shelf.AvailableSpaceInShelf(); ;
        }
        return totalSpace;
    }

    //ניקוי מקרר - כל מה שפג תוקך צריך לזרוק לפח!
    public void CleanExpiryDate()
    {
        foreach (var shelf in Shelves)
        {
            shelf.Items.RemoveAll(item => item.ExpiryDate < DateTime.Now);
        }
    }

    //מה בא לי לאכול? פונקציה שמקבלת כשרות וסוג אוכל ומחזירה אליו מאכלים כאלה קיימים במקרר
    //כמובן שלא נחזיר מאכלים פגי תוקף
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
    //פונקציית הכנסת פריט למקרר.
    public void AddItem(Item item)
    {
        foreach (var shelf in Shelves)
        {
            if (shelf.AvailableSpaceInShelf() >= item.SpaceTaken)
            {
                item.ShelfId = shelf.Id;
                shelf.Items.Add(item);
                return;
            }
        }
        Console.WriteLine("there is not enough space");

    }

    //הוצאת פריט מהמקרר - פנקצויה שמקבלת מזהה פריט, מוציאה אותו מהמקרר ומחזירה אותו למשתמש.
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


    //הפונקציה תמיין ותחזיר את כלל המוצרים לפי תאריך תפוגה שלהם (בסדר עולה)
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

    //הפונקציה תמיין ותחזיר את כלל המדפים לפי מקום פנוי שנשאר עליהם (מהגדול לקטן)
    public List<Shelf> SortShelvesBySpace()
    {
        Shelves.Sort((shelf1, shelf2) => shelf2.AvailableSpaceInShelf().CompareTo(shelf1.AvailableSpaceInShelf()));
        return Shelves;
    }

    //הפונקציה תמיין ותחזיר את כלל המקררים לפי מקום פנוי שנשאר בהם (מהגדול לקטן)
    public List<Refrigerator> SortRefrigeratorsBySpace()
    {
        List<Refrigerator> refrigerators = new List<Refrigerator> { this };
        refrigerators.Sort((refrigerator1, refrigerator2) => refrigerator2.AvailableSpaceOnRefrigerator().CompareTo(refrigerator1.AvailableSpaceOnRefrigerator()));
        return refrigerators;
    }

    //מתכוננים לקניות:
    public void CleanForShoping()
    {
        //הפונקציה תבדוק תחילה אם יש 29 סמר פנויים במקרר ואם לא, תמשיך בשאר התהליך.
        if (AvailableSpaceOnRefrigerator() >= 29)
        {
            Console.WriteLine("There is space");
            return;
        }

    // במידה ואין מקום במקרר, יש לזרוק לפח את כל המוצרים פגי התוקף.
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

        // כלל המוצרים החלביים שתוקפם עוד פחות משלושה ימים.
        foreach (var item in allItems)
        {
            if (item.Kashrut == "חלבי" && (item.ExpiryDate - DateTime.Now).Days < 3)
            {
                toDiscard.Add(item);
                Console.WriteLine($"Item {item.Name} has been discarded.");
                RemoveItem(item.Id);

            }
        }
        //כלל המוצרים הבשריים שתוקפם עוד פחות משבוע.
        if (AvailableSpaceOnRefrigerator() < 29)
        {
            foreach (var item in allItems)
            {
                if (item.Kashrut == "בשרי" && (item.ExpiryDate - DateTime.Now).Days < 7)
                {
                    toDiscard.Add(item);
                    Console.WriteLine($"Item {item.Name} has been discarded.");
                    RemoveItem(item.Id);
                }
            }
        }
        else
        {
            Console.WriteLine("There is space");
            return;
        }
        //כלל המוצרים הפרווה שתוקפם עוד פחות מיום.
        if (AvailableSpaceOnRefrigerator() < 29)
        {
            foreach (var item in allItems)
            {
                if (item.Kashrut == "פרווה" && (item.ExpiryDate - DateTime.Now).Days < 1)
                {
                    toDiscard.Add(item);
                    Console.WriteLine($"Item {item.Name} has been discarded.");
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
        //אם לא מתפנה מקום במקרר, יש להשאיר את המוצרים שעדיין
        //לא פג תוקפם ולהחזיר למשתמש הודעה שזה לא הזמן לעשות קניות
        else
        {
            allItems.AddRange(toDiscard);
            Console.WriteLine("There is still not enough space in the refrigerator so The expired products have been returned to the fridge, you will shop another day. .");
        }
    }

    public override string ToString()
    {
        return $"Refrigerator ID: {Id}, Model: {Model}, Color: {Color}, Number of Shelves: {NumShelves}";
    }
}
