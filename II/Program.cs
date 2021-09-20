// See https://aka.ms/new-console-template for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

ArrayList intArray = new();
ArrayList studentuBaltaisSaraksts = new();
ArrayList studentuMelnaisSaraksts = new();
Queue<int> intQueue = new();
Stack<int> intStack = new();
Random rand = new();
var inLoop = false;
var inLoopStudent = false;
var inLoopQueue = false;
var inLoopStack = false;
var app_on = true;


while (app_on)
{
    Console.WriteLine("izvēlieties uzdevumu: ");
    Console.WriteLine("1. ArrayList int");
    Console.WriteLine("2. Studenti");
    Console.WriteLine("3. Queue");
    Console.WriteLine("4. Stack");
    Console.WriteLine("5. Exit");
    var i = Convert.ToInt32(Console.ReadLine());
    switch (i)
    {
        case 1:
            inLoop = true;
            Tasks();
            break;
        case 2:
            inLoopStudent = true;
            Tasks();
            break;
        case 3:
            inLoopQueue = true;
            Tasks();
            break;
        case 4:
            inLoopStack = true;
            Tasks();
            break;
        case 5:
            Environment.Exit(0);
            break;
    }
    
}

void Tasks()
{
    while (inLoop)
    {
        Console.WriteLine("How many numbers add to array?");
        var amount = Console.ReadLine() ?? string.Empty;
        if (amount != string.Empty && IsDigitsOnly(amount))
        {
            var temp = Convert.ToInt64(amount);
            if (temp > int.MaxValue)
                temp = int.MaxValue;

            for (var i = 0; i < temp; i++) intArray.Add(rand.Next());
        }

        if (intArray.Count < 1)
        {
            Console.WriteLine("Array jābūt skaitļie, lai ar to veiktu manipulācijas! :(");
            continue;
        }

        Console.WriteLine("List elements?");
        if (Console.ReadLine() == "yes")
            Output(intArray);
        Console.WriteLine("Sakārtošana (nospied pogu, lai uzsāktu)");
        Console.ReadKey();
        intArray.Sort(Comparer<int>.Default);
        Output(intArray);
        Console.WriteLine("Izdzēst skaitļi?");
        if (Console.ReadLine() == "yes")
            intArray.RemoveAt(GetIndex(intArray));
        Output(intArray);
        Console.WriteLine("Apgriezt (nospied pogu, lai uzsāktu)");
        Console.ReadKey();
        intArray.Reverse();
        Output(intArray);
        Console.WriteLine("Izdzēst apgabalu?");
        if (Console.ReadLine() == "yes")
        {
            Console.Write("Min Index: ");
            var min = GetIndex(intArray);
            Console.Write("Max Index: ");
            var max = GetIndex(intArray);
            if (max < min)
            {
                var temp = max;
                min = max;
                max = temp;
            }

            intArray.RemoveRange(min, max - min);
            //izdzēst arī min range.
            intArray.RemoveAt(min);
            Output(intArray);
        }

        Console.WriteLine("Pievienot jaunu elementu specifiskā vietā?");
        if (Console.ReadLine() == "yes")
        {
            var index = GetIndex(intArray);
            intArray.Insert(index, rand.Next());
            Output(intArray);
            Console.WriteLine("pievienot veselu kopu?");
            var elem_daudzums = 0;
            if (Console.ReadLine() == "yes")
            {
                Console.Write("Elementu skaits: ");
                elem_daudzums = Convert.ToInt32(Console.ReadLine());
                intArray.AddRange(CreateAMassive(elem_daudzums));
                Output(intArray);
            }

            Console.WriteLine("pievienot veselu kopu specifiskā vietā?");
            if (Console.ReadLine() == "yes")
            {
                index = GetIndex(intArray);
                Console.Write("Elementu skaits: ");
                elem_daudzums = Convert.ToInt32(Console.ReadLine());
                intArray.InsertRange(index, CreateAMassive(elem_daudzums));
                Output(intArray);
            }

            Console.WriteLine("Iztīrīt sarakstu?");
            if (Console.ReadLine() == "yes")
            {
                intArray.Clear();
                Output(intArray);
                Console.WriteLine("Turpināt?");
                if (Console.ReadLine() != "yes")
                    inLoop = false;
            }
        }
    }
    while (inLoopStudent)
    {
        Console.WriteLine("---- Pievienot Studentu ----");
        Console.WriteLine("1. Baltais Saraksts");
        Console.WriteLine("2. Melnais Saraksts");
        gatekeep:
        var key = Console.ReadKey().Key switch
        {
            ConsoleKey.D1 => 1,
            ConsoleKey.NumPad1 => 1,
            ConsoleKey.D2 => 2,
            ConsoleKey.NumPad2 => 2,
            _ => 0
        };
        if (key == 0)
            goto gatekeep;
        Console.WriteLine("Ievadiet Vārdu: ");
        var name = Console.ReadLine();
        Console.WriteLine("Ievadiet Uzvārdu: ");
        var surname = Console.ReadLine();
        switch (key)
        {
            case 1:
                studentuBaltaisSaraksts.Add(new Student(name, surname));
                break;
            case 2:
                studentuMelnaisSaraksts.Add(new Student(name, surname));
                break;
        }

        Console.WriteLine("Pietiek?");
        if (Console.ReadLine() != "yes")
            continue;
        Console.WriteLine("Baltais Saraksts: ");
        Output(studentuBaltaisSaraksts);
        Console.WriteLine("Melnais Saraksts: ");
        Output(studentuMelnaisSaraksts);
        Console.WriteLine("Pārvietot?");
        if (Console.ReadLine() == "yes")
        {
            bool p = true;
            while (p)
            {
                Console.WriteLine("Saraksts: ");
                Console.WriteLine("\t 1. Baltais Saraksts");
                Console.WriteLine("\t 2. Melnais Saraksts");
                var saraksts = Convert.ToInt32(Console.ReadLine());
                sec_check:
                var index = saraksts switch
                {
                    1 => GetIndex(studentuBaltaisSaraksts),
                    2 => GetIndex(studentuMelnaisSaraksts),
                    _ => 0
                };
                MoveStudent(saraksts,index);
                Console.WriteLine("Pārtraukt?");
                if (Console.ReadLine() == "yes")
                    p = false;
            }
        }

        Console.WriteLine("Saglabā .....");
        using var reader = new StreamWriter(@"/home/niks-skersts/someText",false);
        foreach (Student students in studentuBaltaisSaraksts)
        {
            reader.WriteLine("{0};{1}",students.name,students.surname);
        }
        reader.Write("\n:::\n");
        foreach (Student students in studentuMelnaisSaraksts)
        {
            reader.WriteLine("{0};{1}",students.name,students.surname);
        }
        reader.Close();

        Console.WriteLine("Nolasīt? (yes/no)");
        if (Console.ReadLine() != "yes") continue;
        studentuBaltaisSaraksts.Clear();
        studentuMelnaisSaraksts.Clear();
        using var reader_r = new StreamReader(@"/home/niks-skersts/someText",false);
        bool isBaltais = true;
        while (!reader_r.EndOfStream)
        {
            var line = reader_r.ReadLine();
            switch (line)
            {
                case "":
                    continue;
                case ":::":
                    isBaltais = false;
                    continue;
            }
            string[] split = line.Split(";");
            if (isBaltais)
            {
                studentuBaltaisSaraksts.Add(new Student(split[0], split[1]));
            }
            else
            {
                studentuMelnaisSaraksts.Add(new Student(split[0], split[1]));
            }
        }

        Console.WriteLine("Baltais Saraksts: ");
        Output(studentuBaltaisSaraksts);
        Console.WriteLine("Melnais Saraksts: ");
        Output(studentuMelnaisSaraksts);
        Console.WriteLine("Turpināt?");
        if (Console.ReadLine() != "yes")
            inLoopStudent = false;
    }
    while (inLoopQueue)
    {
        for (int i = 0; i < 10; i++)
        {
            intQueue.Enqueue(rand.Next());
        }
        foreach (var num in intQueue)
        {
            Console.WriteLine("Elements: {0}",num);
        }
        Console.WriteLine("Cik elementus izņemt?");
        var elem_daudzums = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < elem_daudzums; i++)
        {
            intQueue.Dequeue();
        }
        foreach (var num in intQueue)
        {
            Console.WriteLine("Elements: {0}",num);
        }

        Console.WriteLine("ievadiet elementu kuru atrast");
        var elem = Convert.ToInt32(Console.ReadLine());
        if (intQueue.Contains(elem))
        {
            Console.WriteLine("Atrasts: {0} ",elem);
        }
        else
        {
            Console.WriteLine("Not found");
        }

        Console.WriteLine("Saraksta info: \n Daudzums: {0}",intQueue.Count);
        foreach (var num in intQueue)
        {
            Console.WriteLine("\t Elements: {0}",num);
        }
        intQueue.Clear();
        inLoopQueue = false;
    }

    while (inLoopStack)
    {
        for (int i = 0; i < 10; i++)
        {
            intStack.Push(rand.Next());
        }

        var arr = intStack.ToArray();
        foreach (var num in arr)
        {
            Console.WriteLine("Elements: {0}",num);
        }
        intStack.Clear();
        inLoopStack = false;
    }
}

void MoveStudent(int saraksts, int index)
{
    switch (saraksts)
    {
        case 1:
            studentuMelnaisSaraksts.Add(studentuBaltaisSaraksts[index]);
            studentuBaltaisSaraksts.RemoveAt(index);
            break;
        case 2:
            studentuBaltaisSaraksts.Add(studentuMelnaisSaraksts[index]);
            studentuBaltaisSaraksts.RemoveAt(index);
            break;
    }
}
void Output(ArrayList arrayList)
{
    var i = 0;
    foreach (var item in arrayList)
    {
        switch (item)
        {
            case int:
                Console.WriteLine("index:{0}, number:{1}", i, item);
                break;
            case Student student:
                Console.WriteLine("\t index: {0}, \n\t name:{1}, \n\t surname: {2}", i, student.name, student.surname);
                break;
        }

        i++;
    }
}

bool IsDigitsOnly(string str)
{
    return str.All(c => c is >= '0' and <= '9');
}

Collection<int> CreateAMassive(int amount)
{
    var coll = new Collection<int>();
    for (var i = 0; i < amount - 1; i++) coll.Add(rand.Next());

    if (coll.Count < 1)
        coll.Add(rand.Next());
    return coll;
}

int GetIndex(ICollection arrayList)
{
    security_check:
    Console.WriteLine("Ievadiet Indeksu");
    var index = Console.ReadLine() ?? string.Empty;
    if (!IsDigitsOnly(index))
        goto security_check;
    var tempIndex = Convert.ToInt32(index);
    if (tempIndex > arrayList.Count - 1 || tempIndex < 0)
        goto security_check;
    return tempIndex;
}

internal class Student
{
    public string name { get; }
    public string surname { get; }

    public Student(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
    }
}