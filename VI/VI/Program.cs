using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

Dictionary<string, Auto> autoList = new()
{
    {"one", new Auto("Auto!", 666)},
    {"two", new Auto("a", 55)},
    {"three", new Auto("b", 5)},
    {"four", new Auto("c", 545)},
    {"five", new Auto("d", 48)},
    {"six", new Auto("e", 123)},
    {"seven", new Auto("f", 55)},
};
Hashtable booklist = new()
{
    {"1",new Book("one",10)},
    {"2",new Book("two",20)},
    {"3",new Book("three",30)},
    {"4",new Book("four",40)},
    {"5",new Book("five",50)},
    {"6",new Book("six",60)},
};

MainMenu();
void MainMenu()
{
    bool on = true;
    int selection = 0;
    int task = 1;
    var myEnumMemberCount = Enum.GetNames(typeof(Buttons)).Length-1;
    IterateThroughButtons();
    while (on)
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow :
                selection++;
                CheckBounds();
                IterateThroughButtons();
                break;
            case ConsoleKey.DownArrow:
                selection--;
                CheckBounds();
                IterateThroughButtons();
                break;
            case ConsoleKey.Enter:
                SelectionExecute();
                break;
        }
    }

    void SelectionExecute()
    {
        switch (selection)
        {
            case 0:
                if (task == 1)
                {
                    sec:
                    var key = GetKey();
                    if(autoList.ContainsKey(key))
                        goto sec;
                    autoList.Add(key,new Auto());
                }
                else
                {
                    sec:
                    var key = GetKey();
                    if(booklist.ContainsKey(key))
                        goto sec;
                    booklist.Add(key,new Book());
                }
                break;
            case 1:
                var delkey = GetKey();
                if (task == 1)
                {
                    if (autoList.Remove(delkey))
                    {
                        goto end;
                    }
                }
                else
                {
                    if (booklist.ContainsKey(delkey))
                    {
                        booklist.Remove(delkey);
                        goto end;
                    }
                }
                Console.WriteLine("Not Found");
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("Items: ");
                if (task == 1)
                {
                    if (autoList.Count != 0)
                    {
                        foreach (var item in autoList)
                        {
                            Console.WriteLine(" \tAuto: {0} \n\t {1} \n\t {2}", item.Key, item.Value.Nosaukums,
                                item.Value.FuelCapacity.ToString());
                        }
                        goto end;
                    }
                }
                else
                {
                    if (booklist.Count !=0)
                    {
                        foreach (DictionaryEntry obj in booklist)
                        {
                            var b = (Book) obj.Value;
                            Console.WriteLine("Key: {0}" +
                                              "\n\t Name: {1}" +
                                              "\n\t Page Count: {2}",
                                obj.Key,b.Nosaukums,b.PageCount.ToString());
                        }
                        goto end;
                    }
                }
                Console.WriteLine("EMPTY!");
                break;
            case 3:
                PrintKeys();
                break;
            case 4:
                PrintValues();
                break;
            case 5:
                ElementAt(GetKey());
                break;
            case 6:
                if (task == 1)
                {
                    autoList.Clear();
                }
                else
                {
                    booklist.Clear();
                }
                break;
            case 7:
                task = task == 1 ? 2 : 1;
                break;
        }
        end:
        Console.WriteLine("Press any key");
        Console.ReadKey();
        IterateThroughButtons();
        
    }
    string GetKey()
    {
        sec:
        Console.WriteLine("Input unique key");
        var key = Console.ReadLine();
        if(string.IsNullOrEmpty(key))
            goto sec;
        return key;
    }
    void PrintKeys()
    {
        var list = autoList.Keys.ToList();
        Console.WriteLine("Keys in dictionary");
        foreach (var key in list)
        {
            Console.WriteLine("\t{0}",key);
        }
        Console.WriteLine("Press Any Key!");
        Console.ReadLine();
        IterateThroughButtons();
    }
    void PrintValues()
    {
        Console.Clear();
        var list = autoList.Values.ToList();
        Console.WriteLine("Keys in dictionary");
        foreach (var val in list)
        {
            Console.WriteLine("Name: {0}\n\t Fuel Capacity: {1}",val.Nosaukums,val.FuelCapacity.ToString());
        }

        Console.WriteLine("Press Any Key!");
        Console.ReadLine();
        IterateThroughButtons();
    }
    void CheckBounds()
    {
        if (selection > myEnumMemberCount)
        {
            selection = myEnumMemberCount;
        }else if (selection < 0)
        {
            selection = 0;
        }

        if (task == 2)
        {
            if (selection is 3)
            {
                selection = 5;
            }

            if (selection is 4)
            {
                selection = 2;
            }
        }
    }
    void IterateThroughButtons()
    {
        Console.Clear();
        int i = 0;
        Console.WriteLine("Selection: {0}", selection);
        Console.WriteLine("Current Task: {0}",task);
        foreach (var button in Enum.GetNames(typeof(Buttons)))
        {
            if (task == 2)
            {
                if (selection is 4 or 3)
                {
                    continue;
                }
            }
            if (selection==i)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(button);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            i++;
        }
    }
    void ElementAt(string index)
    {
        if (task ==1)
        {
            if (!autoList.Any(x => x.Key.Equals(index)))
                goto end;
            var element = autoList.Single(x => x.Key.Equals(index));
            Console.WriteLine("{0} : \n\t Name: {1}\n\t Fuel Capacity: {2}",element.Key,element.Value.Nosaukums,element.Value.FuelCapacity.ToString());
        }
        else
        {
            if (!booklist.ContainsKey(index))
                goto end;
            var element = booklist.OfType<DictionaryEntry>().Where(de => de.Key.Equals(index));
            foreach (var a in element)
            {
                var b = (Book) a.Value;
                Console.WriteLine("Key: {0}" +
                                  "\n\t Name: {1}" +
                                  "\n\t Page Count: {2}",
                    a.Key,b.Nosaukums,b.PageCount.ToString());
            }
        }
        end:
        Console.WriteLine("No Such Element found!");
        Console.WriteLine("Press Any key");
        Console.ReadKey();
        IterateThroughButtons();
        
    }
}

internal enum Buttons
{
    Add,
    Delete,
    Print,
    PrintKeys,
    PrintValues,
    PrintElemAt,
    DeleteAll,
    ChangeTask

}

internal class Auto
{
    public string Nosaukums { get; }
    public double FuelCapacity { get; }

    public Auto()
    {
        Console.WriteLine("Input Name");
        Nosaukums =Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Input Fuel Capacity: ");
        FuelCapacity = Convert.ToDouble(Console.ReadLine());
    }

    public Auto(string name, double fuel)
    {
        Nosaukums = name;
        FuelCapacity = fuel;
    }
}

internal class Book
{
    public string Nosaukums { get; }
    public int PageCount { get; }

    public Book()
    {
        Console.WriteLine("Input Name");
        Nosaukums =Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Input page count: ");
        PageCount = Convert.ToInt32(Console.ReadLine());
    }

    public Book(string name, int lpp)
    {
        Nosaukums = name;
        PageCount = lpp;
    }
}

