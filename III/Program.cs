using System;
using System.Collections;
using System.Collections.Generic;
using static System.Int32;

namespace III
{
    internal static class Program
    {
        private static int Selected { get; set; } = 0;
        private static Mylist Books = new();
        private static List<Choice> Buttons = new()
        {
            new Choice(0,"Print"),
            new Choice(1,"Add"),
            new Choice(2,"Insert"),
            new Choice(3,"Count"),
            new Choice(4,"RemoveAt"),
            new Choice(5,"ElementAt"),
            new Choice(6,"FirstIndexOf"),
            new Choice(7,"LastIndexOf"),
            new Choice(8,"YEEEEEEEEEEEEEEEEEEEET")
        };

        private static void Main(string[] args)
        {
            Books.Add(new Book("Life of Brian","Monty Python",33333));
            Books.Add(new Book("A new Decade","George of the Hill",33333));
            Books.Add(new Book("Higher Mathematics","Dr. Evil",33333));
            Books.Add(new Book("Philosophy and comp.sci","Nicola Tesla & Friends",33333));
            Books.Add(new Book("History of Talavan Kings","The dude who wrote Indriķa Hronika",33333));
            var mainLoop = true;
            var last = MaxValue;
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
                    Books.Print();
                    break;
                case 1:
                    Books.Add(new Book());
                    break;
                case 2:
                    Console.WriteLine("Input ID: ");
                    var i =Convert.ToInt32(Console.ReadLine());
                    Books.Insert(new Book(),i);
                    break;
                case 3:
                    Console.WriteLine("Count: {0}",Books.Count());
                    break;
                case 4:
                    Console.WriteLine("Input ID: ");
                    var removeat =Convert.ToInt32(Console.ReadLine());
                    Books.RemoveAt(removeat);
                    break;
                case 5:
                    Console.WriteLine("Input ID: ");
                    var elementat =Convert.ToInt32(Console.ReadLine());
                    var book =Books.ElementAt(elementat);
                    book.Output();
                    break;
                case 6:
                    Console.WriteLine(Books.FirstIndexOf(new Book()));
                    break;
                case 7:
                    Console.WriteLine(Books.LastIndexOf(new Book()));
                    break;
                case 8:
                    Books.Clear();
                    break;
            }
            Console.Write("Press any key....");
            Console.ReadKey();
            return MaxValue;
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
    internal class Book
    {
        public readonly string Name;
        public readonly string Author;
        public int? Pages;

        public Book(string name,string author, int pages)
        {
            Name = name;
            Author = author;
            Pages = pages;
        }

        public Book(string name, string author)
        {
            Name = name;
            Author = author;
        }

        public Book()
        {
            Console.WriteLine("Input Name: ");
            Name = Console.ReadLine();
            Console.WriteLine("Input Author: ");
            Author = Console.ReadLine();
            Console.WriteLine("Input Page Number: ");
            Pages = Convert.ToInt32(Console.ReadLine());
        }

        public void Output()
        {
            Console.WriteLine(" Name of the book : {0}, \n\t Authored by: {1}",Name,Author);
            if (Pages.HasValue)
                Console.WriteLine("\t Page count: {0}",Pages);
        }
    }

    internal class Item
    {
        public Item NextItem;
        public Book G;
    }

    internal class Mylist
    {
        private Item _firstItem;
        public void Add(Book g1)
        {
            var it = new Item
            {
                G = g1
            };
            if (_firstItem == null)
            {
                _firstItem = it;
                return;
            }
            var current = _firstItem;
            while (current.NextItem != null)
            {
                current = current.NextItem;
            }
            current.NextItem = it;
        }
        public void Print()
        {
            var current = _firstItem;
            while (current != null)
            {
                current.G.Output();
                current = current.NextItem;
            }
        }
        public void Insert(Book g, int index)
        {
            var it = new Item
            {
                G = g
            };
            if (_firstItem == null)
            {
                _firstItem = it;
                return;
            }
            if (index == 0)
            {
                it.NextItem = _firstItem;
                _firstItem=it;
                return;
            }
            var c = 0;
            var current = _firstItem;
            while (current.NextItem != null && c < index-1)
            {
                current = current.NextItem;
                c++;
            }                               
            it.NextItem = current.NextItem;
            current.NextItem = it;       
        }
        public int Count()
        {
            var c = 0;
            var current = _firstItem;
            while (current != null)
            {
                current = current.NextItem;
                c++;
            }
            return c;
        }
        public void RemoveAt(int index)
        {
            if (_firstItem != null)
            {
                _firstItem = _firstItem.NextItem;
                return;
            }
            var c = 0;
            var current = _firstItem;
            while (current is {NextItem: { }} && c < index - 1)
            {
                current = current.NextItem;
                c++;
            }

            if (current is {NextItem: { }}) current.NextItem = current.NextItem.NextItem;
        }
        public Book ElementAt(int index)
        {
            var c = 0;
            var current = _firstItem;
            while (current != null && c != index)
            {
                current = current.NextItem;
                c++;
            }
            return current?.G;
        }
        public int FirstIndexOf(Book g1)
        {
            var c = 0;
            var current = _firstItem;
            while (current != null)
            {
                if (current.G.Author == g1.Author&&current.G.Name==g1.Name&&current.G.Pages==g1.Pages)
                {
                    return c;
                }
                current = current.NextItem;
                c++;
            }
            return -1;
        }
        public int LastIndexOf(Book g1)
        {
            var c = 0;
            var current = _firstItem;
            var index = -1;
            while (current != null)
            {
                if (current.G.Author == g1.Author && current.G.Name == g1.Name && current.G.Pages == g1.Pages)
                {
                    index = c;
                }
                current = current.NextItem;
                c++;
            }
            return index;
        }
        public void Clear()
        {
            _firstItem = null;
        }
    }
}