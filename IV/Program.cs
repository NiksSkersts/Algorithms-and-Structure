// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;


internal static class Program
{
    private static int Selected { get; set; } = 0;
    static MyList list = new MyList();
    private static List<Choice> Buttons = new()
    {
        new Choice(0,"Add"),
        new Choice(1,"PrintAZ"),
        new Choice(2,"PrintZA"),
        new Choice(3,"Insert"),
        new Choice(4,"Count"),
        new Choice(5,"RemoveAt"),
        new Choice(6,"ElementAt"),
        new Choice(7,"YEEEEEEEEEEEEEEEEEEEET")
    };
            private static void Main(string[] args)
        {
            list.Add(new Pietura(0,"Monty Python","SDA"));
            list.Add(new Pietura(1,"George of the Hill",null));
            list.Add(new Pietura(2,"Dr. Evil","4151"));
            list.Add(new Pietura(3,"Nicola Tesla & Friends","11:51"));
            list.Add(new Pietura(4,"The dude who wrote Indriķa Hronika","13:54"));
            var mainLoop = true;
            var last = Int32.MaxValue;
            while (mainLoop)
            {
                if (last != Selected)
                {
                    Console.Clear();
                    Console.WriteLine("Selected: {0}",Selected);
                    foreach (var button in Buttons)
                    {
                        if (Selected == button.Id)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine(button.Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    last = Selected;
                }

                switch (Console.ReadKey())
                {
                    case {Key: ConsoleKey.UpArrow}:
                        if(Selected > 0)
                            Selected--;
                        break;
                    case {Key: ConsoleKey.DownArrow}:
                        if (Selected < Buttons.Count-1)
                            Selected++;
                        break;
                    case {Key: ConsoleKey.Enter}:
                        last = SelectedEntryExec(Selected);
                        break;
                }
            }
        }

        private static int SelectedEntryExec(int id)
        {
            Console.Clear();
            switch(id)
            {
                case 0:
                    list.Add(new Pietura(list.Size));
                    break;
                case 1:
                    list.PrintAZ();
                    break;
                case 2:
                    list.PrintZA();
                    break;
                case 3:
                    Console.WriteLine("Input ID: ");
                    var i =Convert.ToInt32(Console.ReadLine());
                    list.Insert(new Pietura(i),i);
                    break;
                case 4:
                    Console.WriteLine("Count: {0}",list.Size);
                    break;
                case 5:
                    Console.WriteLine("Input ID: ");
                    var removeat =Convert.ToInt32(Console.ReadLine());
                    list.RemoveAt(removeat);
                    break;
                case 6:
                    Console.WriteLine("Input ID: ");
                    var elementat =Convert.ToInt32(Console.ReadLine());
                    var pietura =list.ElementAt(elementat);
                    pietura?.Output();
                    break;
                case 7:
                    list.Clear();
                    break;
            }
            Console.Write("Press any key....");
            Console.ReadKey();
            return Int32.MaxValue;
        }
}



internal class Choice
{
    public readonly int Id;
    public readonly string Name;
    public Choice(int id, string name)
    {
        Id = id;
        Name = name;
    }
        
}
internal class Pietura
{
    public int count { get; set; }
    public string name;
    public string? time;

    public Pietura(int count)
    {
        Console.WriteLine("Input Name: ");
        name = Console.ReadLine() ?? "Empty";
        Console.WriteLine("Input Author: ");
        time = Console.ReadLine() ?? "*crickets*";
        this.count = count;
    }
    public Pietura(int count,string name, string? time)
    {
        this.count = count;
        this.name = name;
        this.time = time;
    }
    public void Output()
    {
        Console.WriteLine("{0} : {1} \n\t {2}",count,name,time);
    }
}

internal class Item
{
    //constructor for the first item
    public Item(Pietura current) => this.Current = current;

    public Item(Item last, Pietura current)
    {
        _lastItem = last;
        Current = current;
    }

    public Item(Item last, Pietura current,Item nextItem)
    {
        _lastItem = last;
        Current = current;
        this._nextItem = nextItem;
    }

    //last item - nullable
    public Item? _lastItem;
    public Pietura Current;
    //next item - nullable
    public Item? _nextItem;
}

internal class MyList
{
    private Item? _first;
    private Item? _last;
    public int Size => GetSize();

    private void SortList()
    {
        if (_first is null) return;
        var current = _first;
        var i = 0;
        while (current != null)
        {
            current.Current.count = i;
            current = current._nextItem;
            i++;
        }
        
    }
    private int GetSize()
    {
        if (_first == null)
        {
            return 0;
        }
        
        var current = _first;
        var i = 0;
        while (current != null)
        {
            current = current._nextItem;
            i++;
        }

        return i;

    }
    public void Add(Pietura pietura)
    {
        if (_first == null)
        {
            _first = new Item(pietura);
            return;
        }

        var current = _first;
        while (current._nextItem != null)
        {
            current = current._nextItem;
        }

        var nxt = new Item(current, pietura);
        _last = nxt;
        current._nextItem = nxt;
        SortList();
    }

    public void PrintAZ()
    {
        var current = _first;
        while (current != null)
        {
            current.Current.Output();
            current = current._nextItem;
        }
    }
    public void PrintZA()
    {
        var current = _last;
        while (current != null)
        {
            current.Current.Output();
            current = current._lastItem;
        }
    }
    public void Insert(Pietura pietura, int index)
    {
        var it = new Item(pietura);
        if (_first == null)
        {
            _first = it;
            return;
        }
        if (index == 0)
        {
            it._nextItem = _first;
            _first=it;
            return;
        }
        if (index == Size)
        {
            Add(pietura);
        }
        var c = 0;
        Item current;
        if (index <= Size/2)
        {
            current = _first;
            while (current._nextItem != null && c < index-1)
            {
                current = current._nextItem;
                c++;
            }
        }
        else
        {
            current = _last;
            while (current._lastItem != null && c < index+1)
            {
                current = current._lastItem;
                c++;
            }   
        }
        it._nextItem = current._nextItem;
        current._nextItem = it;  
        SortList();
    }

    public void RemoveAt(int index)
    {
        if (index == 0 && _first!=null)
        {
            _first = _first._nextItem;
            return;
        }
        var c = 0;
        Item? current;
        if (index <= Size)
        {
            current = _first;
            while (current?._nextItem is not null && c < index-1)
            {
                current = current?._nextItem;
                c++;
            }
        }
        else
        {
            current = _last;
            while (current?._lastItem is not null && c > index+1)
            {
                current = current?._nextItem;
                c++;
            }
        }

        if (current is {_nextItem: { }}) current._nextItem = current._nextItem._nextItem;
        SortList();
        
    }

    public Pietura? ElementAt(int index)
    {
        var c = 0;
        Item? current;
        if (index <= Size)
        {
            current = _first;
            while (current?._nextItem is not null && c < index)
            {
                current = current._nextItem;
                c++;
            }
        }
        else
        {
            current = _last;
            while (current?._lastItem is not null && c > index)
            {
                current = current._nextItem;
                c++;
            }
        }

        return current?.Current;
    }

    public void Clear()
    {
        _first = null;
    }
}