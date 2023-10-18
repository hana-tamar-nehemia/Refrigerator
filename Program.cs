using System;

class Program
{
    static void Main()
    {
        Item item1 = new Item("1234", "מיץ תפוחים", "שתיה", "פרווה", new DateTime(2023, 12, 31), 10);
        Item item2 = new Item("5678", "לחם מרוסק", "אוכל", "חלבי", new DateTime(2023, 10, 20), 20);
        Item item3 = new Item("9101", "אפרסקים", "שתיה", "פרווה", new DateTime(2023, 11, 30), 15);

        // יצירת מדפים
        Shelf shelf1 = new Shelf("1", 1, 30);
        Shelf shelf2 = new Shelf("2", 2, 40);

        // הוספת פריטים למדפים
        shelf1.Items.Add(item1);
        shelf2.Items.Add(item2);
        shelf1.Items.Add(item3);

        // יצירת רשימת מדפים והוספתם למקרר
        List<Shelf> shelves = new List<Shelf> { shelf1, shelf2 };

        // יצירת מקרר
        Refrigerator refrigerator = new Refrigerator("F123", "מקרר סופר", "כחול", 2, shelves);

        while (true)
        {
            Console.WriteLine("לחץ 1: הצגת כל הפריטים והתכולות במקרר");
            Console.WriteLine("לחץ 2: הצגת כמה מקום נשאר במקרר");
            Console.WriteLine("לחץ 3: הכנסת פריט למקרר");
            Console.WriteLine("לחץ 4: הוצאת פריט מהמקרר");
            Console.WriteLine("לחץ 5: ניקוי המקרר והצגת כלל הפריטים שנבדקו");
            Console.WriteLine("לחץ 6: מה בא לך לאכול?");
            Console.WriteLine("לחץ 7: הצגת כל הפריטים מסודרים לפי תאריך תפוגה");
            Console.WriteLine("לחץ 8: הצגת כל המדפים מסודרים לפי מקום פנוי");
            Console.WriteLine("לחץ 9: הצגת כל המקררים מסודרים לפי מקום פנוי");
            Console.WriteLine("לחץ 10: הכנס את המקרר לקניות");
            Console.WriteLine("לחץ 100: סגירת המערכת");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        foreach (var shelf in refrigerator.Shelves)
                        {
                            foreach (var item0 in shelf.Items)
                            {
                                Console.WriteLine($"שם המוצר: {item0.Name}, מדף: {shelf.Id}");
                            }
                        }
                        break;
                    case 2:
                        double space = refrigerator.AvailableSpaceOnRefrigerator();
                        Console.WriteLine($"יש {space} פנויים במקרר");
                        break;
                    case 3:
                        Console.WriteLine("הזן מזהה פריט:");
                        string itemId = Console.ReadLine();

                        while (refrigerator.allItems().Any(item => item.Id == itemId))
                        {
                            Console.WriteLine("כבר קיים פריט עם מזהה זהה.");
                            itemId = Console.ReadLine();
                        }
                        Console.Write("?שם המוצר ");
                        string name = Console.ReadLine();
                        Console.Write("סוג - אוכל או שתיה: ");
                        string type = Console.ReadLine();
                        Console.Write("?כשרות בשרי, חלבי, פרווה ");
                        string kashrut = Console.ReadLine();
                        Console.Write("תאריך תפוגה בפורמט dd/MM/yyyy): ");
                        DateTime expiryDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        Console.Write("גודל?");
                        double size = double.Parse(Console.ReadLine());

                        Item item = new Item(itemId, name, type, kashrut, expiryDate, size);

                        refrigerator.AddItem(item);

                        break;
                    case 4:

                        Console.WriteLine(" הזן מזהה פריט להסרה מהמקרר");
                        string removeItemId = Console.ReadLine();
                        Item itemRemoved = refrigerator.RemoveItem(removeItemId);

                        if (itemRemoved != null)
                        {
                            Console.WriteLine("הפריט הוצא מהמקרר");
                        }
                        else
                        {
                            Console.WriteLine("לא ניתן למצוא פריט עם מזהה זה");
                        }
                        break;
                    case 5:
                        refrigerator.CleanExpiryDate();
                        break;
                    case 6:
                        Console.WriteLine("מה ברצונך לאכול? (אוכל / שתייה)");
                        string typeToEat = Console.ReadLine();

                        Console.WriteLine("איזו כשרות רוצים? (בשרי / חלבי / פרווה)");
                        string kashrutToEat = Console.ReadLine();

                        List<Item> matchingItems = refrigerator.GetItemsByKashrutAndType(kashrutToEat, typeToEat);

                        if (matchingItems.Count > 0)
                        {
                            Console.WriteLine("הפריטים: ");
                            foreach (var matchingItem in matchingItems)
                            {
                                Console.WriteLine(matchingItem.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("אין במקרר");
                        }
                        break;
                    case 7:
                        List<Item> sortedByExpiryDate = refrigerator.SortByExpiryDate();
                        foreach (var sortItem in sortedByExpiryDate)
                        {
                            Console.WriteLine(sortItem.ToString());
                        }
                        break;
                    case 8:
                        List<Shelf> sortedShelvesByFreeSpace = refrigerator.SortShelvesBySpace();
                        foreach (var shelf in sortedShelvesByFreeSpace)
                        {
                            Console.WriteLine(shelf.ToString());
                        }
                        break;
                    case 9:
                        List<Refrigerator> sortedRefrigeratorsByFreeSpace = refrigerator.SortRefrigeratorsBySpace();
                        foreach (var refrigerator1 in sortedRefrigeratorsByFreeSpace)
                        {
                            Console.WriteLine(refrigerator1.ToString());
                        }
                        break;
                    case 10:
                        refrigerator.CleanForShoping();
                        break;
                    case 100:
                        Console.WriteLine("המערכת נסגרה");
                        return;
                    default:
                        Console.WriteLine("נסה שוב");
                        break;
                }
            }
            else
            {
                Console.WriteLine("קלט לא חוקי. נסה שוב.");
            }

            Console.WriteLine("לחץ כל מקש להמשך...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
