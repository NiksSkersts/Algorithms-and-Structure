using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
int[,] Matrix = new int[9, 9];

Tree.Station Dalbe = new("Dalbe");
Tree.Station Ozolnieki = new("Ozolnieki");
Tree.Station Riga = new("Rīga");
Tree.Station Jelgava = new("Jelgava");
Tree.Station Eleja = new("Eleja");
Tree.Station Liepaja = new("Liepāja");
Tree.Station Dobele = new("Dobele");
Tree.Station Tukums = new("Tukums");
Tree.Station Saldus = new("Saldus");

Dalbe.StationData = new List<Tree.StationData>(
    new []
    {
        new Tree.StationData(Riga,12),
        new Tree.StationData(Ozolnieki,5)
    }
    );
Riga.StationData = new List<Tree.StationData>(
    new []
    {
        new Tree.StationData(Dalbe,10),
        new Tree.StationData(Jelgava,45),
        new Tree.StationData(Liepaja,78)
    }
    );
Ozolnieki.StationData = new List<Tree.StationData>(
    new []
    {
        new Tree.StationData(Dalbe,5),
        new Tree.StationData(Jelgava,7)
    });
Jelgava.StationData = new List<Tree.StationData>(
    new []
    {
        new Tree.StationData(Riga,48),
        new Tree.StationData(Dobele,30),
        new Tree.StationData(Ozolnieki,6)
    });
Eleja.StationData = new List<Tree.StationData>(
    new[]
    {
        new Tree.StationData(Jelgava, 22)
    });
Dobele.StationData = new List<Tree.StationData>(
    new[]
    {
        new Tree.StationData(Jelgava, 31),
        new Tree.StationData(Tukums, 22),
        new Tree.StationData(Saldus, 35)
    });
Tukums.StationData = new List<Tree.StationData>(
    new[]
    {
        new Tree.StationData(Dobele, 21),
        new Tree.StationData(Saldus, 18)
    });
Saldus.StationData = new List<Tree.StationData>(
    new []
    {
        new Tree.StationData(Tukums,22),
        new Tree.StationData(Dobele,33),
        new Tree.StationData(Liepaja,55)
    });
Liepaja.StationData = new List<Tree.StationData>(
    new[]
    {
        new Tree.StationData(Riga, 83)
    });
Tree t = new Tree(new List<Tree.Station>(
    new []
    {
        Dalbe,
        Ozolnieki,
        Riga,
        Jelgava,
        Eleja,
        Dobele,
        Tukums,
        Saldus,
        Liepaja
    }
));

var on = true;
var toFromSwitch = false;
int mainselection = 0;
int selection = 0;
Tree.Station fromStation = null;
Tree.Station toStation = null;
IterateThroughButtons();
while (on)
{
    var getSelection = GetSelection(Console.ReadKey().Key);
    if (getSelection!= null)
    {
        switch (toFromSwitch)
        {
            case true:
                toFromSwitch = false;
                toStation = t.Branches[selection];
                SelectionExecute(ref fromStation,ref toStation);
                break;
            case false:
                fromStation = t.Branches[selection];
                toFromSwitch = true;
                break;
        }
        IterateThroughButtons();
    }
}
int? GetSelection(ConsoleKey key)
{
    switch (key)
    {
        case ConsoleKey.UpArrow:
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
            return selection;
    }

    return null;
}
void CheckBounds()
{
    if (selection > t.Branches.Count-1)
        selection = t.Branches.Count-1;
    else if (selection < 0) selection = 0;
}
void IterateThroughButtons()
{
    Console.Clear();
    PrintMatrix();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine(toFromSwitch ? "TO: " : "FROM: ");
    var i = 0;
    foreach (var station in t.Branches)
    {
        if (selection == i)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        Console.WriteLine(station.Name);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        i++;
    }
}

void CollectData()
{
    foreach (var branch_a in t.Branches)
    {
        foreach (var branch_b in t.Branches)
        {
            foreach (var b in branch_b.StationData)
            {
                var index0 = t.Branches.IndexOf(branch_b);
                var index1 = t.Branches.IndexOf(b.ConnectStation);
                Matrix[index0, index1] = b.Cost;
            }
        }
        foreach (var a in branch_a.StationData)
        {
            var index0 = t.Branches.IndexOf(branch_a);
            var index1 = t.Branches.IndexOf(a.ConnectStation);
            Matrix[index0, index1] = a.Cost;
        }
    }
    for (int i = 0; i < Matrix.GetLength(0); i++)
    {
        for (int j = 0; j < Matrix.GetLength(1); j++)
        {
            if (Matrix[i,j]==0)
            {
                Matrix[i, j] = -1;
            }
        }
    }
}
void PrintMatrix()
{
    CollectData();
    for (int i = 0; i < 10; i++)
    {
        Console.Write(" ");
    }
    for (int i = 0; i < t.Branches.Count-1; i++)
    {
        Console.Write(" {0} ",t.Branches.ElementAt(i).Name);
    }
    Console.WriteLine();
    for (int i = 0; i < 8; i++)
    {
        var charcount = t.Branches.ElementAt(i).Name.ToCharArray();
        Console.Write("{0}",t.Branches.ElementAt(i).Name);
        for (int ind = 0; ind < 10 - charcount.Length; ind++)
        {
            Console.Write(" ");
        }
        for (int j = 0; j < 8; j++)
        {
            var charcountJ = t.Branches.ElementAt(j).Name.ToCharArray();

            string a = Matrix[i, j].ToString();
            if (Matrix[i,j] >= 0 && Matrix[i,j]<=9 )
            {
                a += " ";
            }

            for (int k = 0; k <charcountJ.Length/2; k++)
            {
                Console.Write(" ");
            }
            Console.Write("{0}",a);
            for (int k = 0; k <charcountJ.Length/2; k++)
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine();
    }
}
void SelectionExecute(ref Tree.Station to, ref Tree.Station from)
{
    if (from.Name.Equals(to.Name))
    {
        Console.WriteLine("Tiks attēloti visi iespējamie ceļi no {0} - > {1}",from.Name,to.Name);
    }
    var res = t.PrintPath(new List<Tree.Station>()
    {
        to,from
    });
    Dictionary<string, int> costTable = new();
    foreach (var path in res)
    {
        int i = 0;
        int cost = 0;
        string pathString = "";
        foreach (var elem in path)
        {
            if (elem != path.Last() || (elem == path.First() && i == 0))
            {
                var index1 = path.IndexOf(elem) + 1;
                var nextstation = path.ElementAt(index1);
                var sd= elem.StationData.Single(x => x.ConnectStation.Name.Equals(nextstation.Name));
                cost += sd.Cost;
                pathString = pathString + elem.Name + " -> ";
            }
            else
            {
                if (i != 0)
                {
                    pathString += elem.Name;
                }
            }

            i++;
        }
        costTable.Add(pathString,cost);
    }

    if (!costTable.Any())
        goto end;
    var min = costTable.Values.Min();
    foreach (var var in costTable)
    {
        string append = String.Empty;
        if (var.Value == min)
        {
            append = "(Lētākais!!!)";
        }
        Console.WriteLine("\n" +
                          "{0}" +
                          "\n\t Total Cost: {1} {2}" +
                          "\n",var.Key, var.Value,append);
    }
    end:
    Console.WriteLine("Press Any Key");
    Console.ReadKey();
}


internal class Tree
{
    private List<Stack<Station>> Path = new();
    public class Station
    {
        public string Name;
        public List<StationData> StationData;
        public Station(string n)
        {
            Name = n;
        }
    }
    public class StationData
    {
        public Station ConnectStation;
        public int Cost;

        public StationData(Station station, int c)
        {
            ConnectStation = station;
            Cost = c;
        }
    }
    
    public List<Station> Branches = new();

    public Tree(List<Station> branch)
    {
        Branches.AddRange(branch);
    }
    public List<List<Station>> PrintPath(List<Station> list)
    {
        List<List<Station>> fullPaths = new();
        List<Station> temp = new(list);
        Station current = list.ElementAt(list.Count - 2);
        foreach (var SD in current.StationData)
        {
            if (list.Last().Name.Equals(SD.ConnectStation.Name))
            {
                fullPaths.Add(new List<Station>(temp));
            }
            if (!list.Any(x=>x.Name.Equals(SD.ConnectStation.Name)))
            {
                temp.Insert(list.IndexOf(current)+1,SD.ConnectStation);
                var res = PrintPath(temp);
                fullPaths.AddRange(res);
                temp.RemoveAt(list.IndexOf(current)+1);
            }
        }
        return fullPaths;
    }
}