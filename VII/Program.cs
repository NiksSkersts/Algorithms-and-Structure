using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;


MainMenu();
void MainMenu()
{
    Koks koks = new Koks(
        new List<Zars>()
        {
            new Zars(new Rajons("Jelgava","5000"),new List<Zars>()
            {
                new Zars(new Rajons("Lielā iela","100"),new List<Zars>()),
                new Zars(new Rajons("Pasta iela","100"),new List<Zars>(
                    new []
                    {
                        new Zars(new Rajons("Pasta iela 1","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 2","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 3","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 4","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 5","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 6","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 7","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 8","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 9","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 10","10"),new List<Zars>()),
                    }
                )),
                new Zars(new Rajons("Alda iela","100"),new List<Zars>()),
                new Zars(new Rajons("Jēkaba iela","100"),new List<Zars>()),
                new Zars(new Rajons("Alus iela","100"),new List<Zars>()),
                new Zars(new Rajons("Nu nez iela","100"),new List<Zars>())
            }),
            new Zars(new Rajons("Rīga","5000000"),new List<Zars>()
            {
                new Zars(new Rajons("Lielā iela","100"),new List<Zars>()),
                new Zars(new Rajons("Pasta iela","100"),new List<Zars>(
                    new []
                    {
                        new Zars(new Rajons("Pasta iela 1","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 2","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 3","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 4","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 5","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 6","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 7","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 8","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 9","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 10","10"),new List<Zars>()),
                    }
                )),
                new Zars(new Rajons("Alda iela","100"),new List<Zars>()),
                new Zars(new Rajons("Jēkaba iela","100"),new List<Zars>()),
                new Zars(new Rajons("Alus iela","100"),new List<Zars>()),
                new Zars(new Rajons("Nu nez iela","100"),new List<Zars>())
            }),
            new Zars(new Rajons("Smiltene","500"),new List<Zars>()
            {
                new Zars(new Rajons("Lielā iela","100"),new List<Zars>()),
                new Zars(new Rajons("Pasta iela","100"),new List<Zars>(
                    new []
                    {
                        new Zars(new Rajons("Pasta iela 1","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 2","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 3","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 4","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 5","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 6","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 7","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 8","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 9","10"),new List<Zars>()),
                        new Zars(new Rajons("Pasta iela 10","10"),new List<Zars>()),
                    }
                )),
                new Zars(new Rajons("Alda iela","100"),new List<Zars>()),
                new Zars(new Rajons("Jēkaba iela","100"),new List<Zars>()),
                new Zars(new Rajons("Alus iela","100"),new List<Zars>()),
                new Zars(new Rajons("Nu nez iela","100"),new List<Zars>())
            })
        });
    bool on = true;
    int selection = 0;
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
                koks.zari.Add(new Zars());
                break;
            case 1:
            {
                sec:
                Console.WriteLine("Kuram rajonam pievienot: ");
                var add = Console.ReadLine() ?? String.Empty;
                if (add == String.Empty)
                    goto sec;
                var raj=koks.Search(add);
                raj?.zari.Add(new Zars(new Rajons(),new List<Zars>()));
            }
                break;
            case 2:
            {
                sec:
                Console.WriteLine("Kuram rajonam labot: ");
                var add = Console.ReadLine() ?? String.Empty;
                if (add == String.Empty)
                    goto sec;
                Zars? rajons = null;
                rajons = koks.Search(add);
                if (rajons==null)
                {
                    Console.WriteLine("Not Found");
                    goto sec;
                }
                Console.WriteLine("Ievadiet jaunos datus: (Null == tiek atstāta iepriekšējā vērtība");
                Console.Write("Nosaukums:" );
                var nosaukums = Console.ReadLine();
                Console.WriteLine("Platība: ");
                var platiba = Console.ReadLine();
                if (!string.IsNullOrEmpty(nosaukums))
                {
                    rajons.rajons.Nosaukums = nosaukums;
                }

                if (!string.IsNullOrEmpty(platiba))
                {
                    rajons.rajons.Platiba = platiba;
                }
            }
                break;
            case 3:
                koks.Print();
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
        end:
        Console.WriteLine("Press any key");
        Console.ReadKey();
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
    }
    void IterateThroughButtons()
    {
        Console.Clear();
        int i = 0;
        foreach (var button in Enum.GetNames(typeof(Buttons)))
        {
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
}

internal enum Buttons
{
    PievienotPamatrajonu,
    PievienotApakšrajonu,
    LabotRajonaDatus,
    IzvadītDatus,
    Beigt

}

class Rajons
{
    public string Nosaukums { get; set; }
    public string Platiba { get; set; }

    public Rajons(string nosaukums, string platiba)
    {
        Nosaukums = nosaukums;
        Platiba = platiba;
    }

    public Rajons()
    {
        Console.WriteLine("Rajona nosaukums");
        Nosaukums = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Rajona platība");
        Platiba = Console.ReadLine() ?? string.Empty;
    }
}

internal class Zars
{
    public Rajons rajons { get; set; }
    public List<Zars> zari { get; set; }

    public Zars(Rajons r, List<Zars> zari)
    {
        rajons = r;
        this.zari = zari;
    }

    public Zars()
    {
        rajons = new Rajons();
        zari = new List<Zars>();
        Console.WriteLine("Apakšrajonu skaits: ");
        var j = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < j; i++)
        {
            zari.Add(new Zars());
        }
    }

}

internal class Koks
{
    public List<Zars> zari=new();

    public Koks(List<Zars> zari)
    {
        this.zari = zari;
    }
    public void Print()
    {
        string indent = "";
        for (int i= 0; i < zari.Count; i++)
        {
            PrintRaj(zari[i],indent,i == zari.Count - 1);
        }
        void PrintRaj(Zars zars,string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\t");
                indent += "\t";
            }
            else
            {
                Console.Write("\t");
                indent += "\t";
            }
            Console.WriteLine("{0}{1}({2})",indent,zars.rajons.Nosaukums,zars.rajons.Platiba);
            for (int i = 0; i < zars.zari.Count; i++)
                PrintRaj(zars.zari[i],indent,i == zars.zari.Count - 1);
        }
    }
    public Zars? Search(string nosaukums)
    {
        foreach (Zars z in zari)
        {
            if (nosaukums == z.rajons.Nosaukums)
            {
                return z;
            }

            foreach (var z1 in z.zari.Where(z1 => nosaukums == z1.rajons.Nosaukums))
            {
                return z1;
            }
        }
        return null;
    }
}

