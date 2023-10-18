using System;

class Program
{
    static void Main()
    {
        Item item1 = new Item("מיץ תפוחים", "שתייה", "פרווה", new DateTime(2023, 12, 31), 10);
        Item item2 = new Item("עוגה" , "אוכל", "חלבי", new DateTime(2023, 10, 20), 20);
        Item item3 = new Item( "אפרסקים", "אוכל", "פרווה", new DateTime(2023, 11, 30), 15);

        // יצירת מדפים
        Shelf shelf1 = new Shelf( 1, 30);
        Shelf shelf2 = new Shelf( 2, 40);

        // הוספת פריטים למדפים
        item1.ShelfId = "1";
        shelf1.Items.Add(item1);
        item2.ShelfId = "2";
        shelf2.Items.Add(item2);
        item3.ShelfId = "1";
        shelf1.Items.Add(item3);

        // יצירת רשימת מדפים והוספתם למקרר
        List<Shelf> shelves = new List<Shelf> { shelf1, shelf2 };

        // יצירת מקרר
        Refrigerator refrigerator = new Refrigerator( "מקרר סופר", "כחול", 2, shelves);
        try
        {
            while (true)
            {
                Console.WriteLine("לחץ 1: הצגת כל הפריטים והתכולות במקרר");
                Console.WriteLine("לחץ 2: הצגת כמה מקום נשאר במקרר");
                Console.WriteLine("לחץ 3: הכנסת פריט למקרר");
                Console.WriteLine("לחץ 4: הוצאת פריט מהמקרר");
                Console.WriteLine("לחץ 5: ניקוי המקרר והצגת כלל הפריטים שנבדקו");
                Console.WriteLine("לחץ 6: מה בא לך לאכול");
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
                                    Console.WriteLine(item0.Name);
                                }
                            }
                            break;


                        case 2:
                            double space = refrigerator.AvailableSpaceOnRefrigerator();
                            Console.WriteLine($"יש   {space}   פנויים במקרר");
                            break;


                        case 3:
                            try
                            {
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

                                Item item = new Item(name, type, kashrut, expiryDate, size);

                                refrigerator.AddItem(item);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("something went wrong in your input, try again");
                            }
                            break;


                        case 4:
                            Console.WriteLine(" הזן מזהה פריט להסרה מהמקרר");
                            string removeItemId = Console.ReadLine();
                            Item itemRemoved = refrigerator.RemoveItem(removeItemId);

                            if (itemRemoved != null)
                            {
                                Console.WriteLine($"הפריט {itemRemoved.Name} הוצא מהמקרר");
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
                                    Console.WriteLine(matchingItem.Name);
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
                                Console.WriteLine($"{sortItem.Name} , {sortItem.Type} , {sortItem.ExpiryDate}");
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
        catch (Exception ex)
        {
            Console.WriteLine("somthing went wrong");
        }
    }
}
