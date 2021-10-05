// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.String;

namespace V
{
    internal static class Program
    {
        private static List<Klients> _list = new();
        private static readonly List<Choice> Buttons = new()
        {
            new Choice(0, "Add"),
            new Choice(1, "Delete"),
            new Choice(2, "Sort"),
            new Choice(3, "RemoveAt"),
            new Choice(4, "InsertAt"),
            new Choice(5, "Print")
        };
        private static int Selected { get; set; }
        private static void Main(string[] args)
        {
            const bool mainLoop = true;
            var last = int.MaxValue;
            _list.Add(new Klients("A@inbox.lv", "Aldis", "Ceriņš", "ACERS", "qwerty", new Pasūtījums[]
            {
                new(DateTime.Now, Status.Nosūtīts, new Piegāde("ALDA IELA", Pilsēta.Jelgava),
                    new Maksājums(11.22, MaksājumaVeids.Pārskaitījums)),
                new(DateTime.Now, Status.Izpildīts, new Piegāde("Alas IELA", Pilsēta.Jelgava),
                    new Maksājums(1210.22, MaksājumaVeids.Pārskaitījums)),
                new(DateTime.Now, Status.Pieņemts, new Piegāde("A IELA", Pilsēta.Jelgava),
                    new Maksājums(10.22, MaksājumaVeids.Pārskaitījums)),
                new(DateTime.Now, Status.Nosūtīts, new Piegāde("B IELA", Pilsēta.Jelgava),
                    new Maksājums(9.22, MaksājumaVeids.Pārskaitījums)),
                new(DateTime.Now, Status.Nosūtīts, new Piegāde("C IELA", Pilsēta.Jelgava),
                    new Maksājums(1.22, MaksājumaVeids.Pārskaitījums))
            }));
            _list.Add(
                new Klients("VBU@inbox.lv", "Vendenūdens", "Baumanis", "ADASSAA", "abcdefg",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("KKKKKKKKKKKKKKKKKK, LV-4726", Pilsēta.Olaine),
                            new Maksājums(69.69, MaksājumaVeids.Skaidrā))
                    }));
            _list.Add(
                new Klients("ASL@inbox.lv", "ASLANS", "THE LION", "witchesbebadgrrr", "baaaaaaaaaaaaaaaaaaaad",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("paradise, I guess, LV-4726", Pilsēta.Rīga),
                            new Maksājums(666.00, MaksājumaVeids.Skaidrā))
                    }));
            _list.Add(
                new Klients("VBU@inbox.lv", "AAA", "BBB", "manjauzb", "sadasdasds",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("sdsds, LV-4726", Pilsēta.Olaine),
                            new Maksājums(69.69, MaksājumaVeids.Skaidrā))
                    }));
            _list.Add(
                new Klients("VBU@sdsd.lv", "Vendenūdens", "Baumanis", "ADASSAA", "abcdefg",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("aaaaaaaaaaaaaaaa, LV-4726", Pilsēta.Olaine),
                            new Maksājums(69.69, MaksājumaVeids.Skaidrā))
                    }));
            _list.Add(
                new Klients("VBU@inbox.lv", "Vendenūdens", "Baumanis", "ADASSAA", "abcdefg",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("bbbbbbbbbb, LV-4726", Pilsēta.Olaine),
                            new Maksājums(69.69, MaksājumaVeids.Skaidrā))
                    }));
            _list.Add(
                new Klients("VBU@inbox.lv", "Vendenūdens", "Baumanis", "ADASSAA", "abcdefg",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("ccccccccccccccccccccc, LV-4726", Pilsēta.Olaine),
                            new Maksājums(69.69, MaksājumaVeids.PayPal))
                    }));
            _list.Add(
                new Klients("VBU@inbox.lv", "T", "BH", "ADASSAA", "abcdefg",
                    new[]
                    {
                        new Pasūtījums(DateTime.Today, Status.Izpildīts,
                            new Piegāde("asdsada, LV-4726", Pilsēta.Rēzekne),
                            new Maksājums(0.01, MaksājumaVeids.Skaidrā))
                    }));
            while (mainLoop)
            {
                if (last != Selected)
                {
                    Clear();
                    WriteLine("Selected: {0}", Selected);
                    foreach (var button in Buttons)
                    {
                        if (Selected == button.Id)
                        {
                            ForegroundColor = ConsoleColor.Black;
                            BackgroundColor = ConsoleColor.White;
                        }

                        WriteLine(button.Name);
                        ForegroundColor = ConsoleColor.White;
                        BackgroundColor = ConsoleColor.Black;
                    }

                    last = Selected;
                }

                switch (ReadKey())
                {
                    case {Key: ConsoleKey.UpArrow}:
                        if (Selected > 0)
                            Selected--;
                        break;
                    case {Key: ConsoleKey.DownArrow}:
                        if (Selected < Buttons.Count - 1)
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
            Clear();
            switch (id)
            {
                case 0:
                    _list.Add(new Klients());
                    break;
                case 1:
                    var klients = new Klients();
                    var klientsEnumerable = _list.Any(p => p.Equals(klients));
                    if (klientsEnumerable)
                    {
                        _list.Remove(klients);
                    }
                    else
                    {
                        WriteLine("User Not Found!");
                    }
                    break;
                case 2:
                    var orderedEnumerable = _list.OrderBy(x => x.Vārds).ThenBy(y => y.Uzvards);
                    _list = orderedEnumerable.ToList();
                    break;
                case 3:
                    WriteLine("Input ID: ");
                    var i = Convert.ToInt32(ReadLine());
                    _list.RemoveAt(i);
                    break;
                case 4:
                    WriteLine("Input ID: ");
                    var j = Convert.ToInt32(ReadLine());
                    _list.Insert(j, new Klients());
                    break;
                case 5:
                    foreach (var k in _list)
                    {
                        WriteLine("─────────────────────");
                        WriteLine("{0}  {1}, \n\t Lietotājvārds: {2}, \n\t {3}", k.Vārds, k.Uzvards, k.lietotājs,
                            k.Email);
                        foreach (var pas in k.Pasūtījumu)
                        {
                            WriteLine();
                            WriteLine("\t Piegāde:");
                            WriteLine("\t\t {0}", pas.Piegāde.adrese);
                            WriteLine("\t\t {0}", pas.Piegāde.Pilsēta);
                            WriteLine("\t Maksājums:");
                            WriteLine("\t\t Veids: {0}", pas.Maksājums.veids);
                            WriteLine("\t\t Summa: {0}", pas.Maksājums.summa);
                            WriteLine("\t Datums: {0}", pas.DateTime);
                            WriteLine("\t Status: {0}", pas.Status);
                            WriteLine();
                        }
                    }

                    break;
            }

            Write("Press any key....");
            ReadKey();
            return int.MaxValue;
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

    internal enum MaksājumaVeids
    {
        Skaidrā = 1,
        Karte = 2,
        PayPal = 3,
        Pārskaitījums = 4
    }

    internal enum Pilsēta
    {
        Jelgava = 1,
        Rīga = 2,
        Olaine = 3,
        Rēzekne = 4,
        Liepāja = 5
    }

    internal enum Status
    {
        Pieņemts = 1,
        Nosūtīts = 2,
        Izpildīts = 3
    }

    internal class Maksājums
    {
        public double summa;
        public MaksājumaVeids veids;

        public Maksājums(double summa, MaksājumaVeids mv)
        {
            this.summa = summa;
            veids = mv;
        }

        public Maksājums()
        {
            WriteLine("Ievadiet summu: ");
            summa = Convert.ToDouble(ReadLine());
            WriteLine("Izvēlieties Maksājuma veidu: ");
            var i = 1;
            foreach (var m in Enum.GetNames(typeof(MaksājumaVeids)))
            {
                WriteLine(i + " " + m);
                i++;
            }

            veids = (MaksājumaVeids) Enum.ToObject(typeof(MaksājumaVeids), Convert.ToInt32(ReadLine()));
        }
    }

    internal class Piegāde
    {
        public string adrese;
        public Pilsēta Pilsēta;

        public Piegāde(string adrese, Pilsēta p)
        {
            this.adrese = adrese;
            Pilsēta = p;
        }

        public Piegāde()
        {
            WriteLine("Ievadiet Addresi: ");
            adrese = ReadLine() ?? Empty;
            var i = 1;
            foreach (var st in Enum.GetNames(typeof(Pilsēta)))
            {
                WriteLine(i + ": " + st);
                i++;
            }

            WriteLine("Ievadiet izvēli (nr): ");
            Pilsēta = (Pilsēta) Enum.ToObject(typeof(Pilsēta), Convert.ToInt32(ReadLine()));
        }
    }

    internal class Pasūtījums
    {
        public DateTime DateTime;
        public Maksājums Maksājums;
        public Piegāde Piegāde;
        public Status Status;

        public Pasūtījums()
        {
            WriteLine("Ievadiet Datumu: ");
            DateTime = Convert.ToDateTime(ReadLine());
            var i = 1;
            foreach (var st in Enum.GetNames(typeof(Status)))
            {
                WriteLine(i + ": " + st);
                i++;
            }

            WriteLine("Izvēlieties statusu (nr): ");
            Status = (Status) Enum.ToObject(typeof(Status), Convert.ToInt32(ReadLine()));
            Piegāde = new Piegāde();
            Maksājums = new Maksājums();
        }

        public Pasūtījums(DateTime dateTime, Status st, Piegāde p, Maksājums maksājums)
        {
            DateTime = dateTime;
            Status = st;
            Piegāde = p;
            Maksājums = maksājums;
        }
    }

    internal class Persona
    {
        public string? Uzvards;
        public string? Vārds;
    }

    internal class Lietotājs : Persona
    {
        public string? lietotājs;
        protected string? Parole;
    }

    internal class Klients : Lietotājs
    {
        public string Email;
        public Pasūtījums[] Pasūtījumu;

        public Klients()
        {
            WriteLine("Vārds: ");
            Vārds = ReadLine() ?? Empty;
            WriteLine("Uzvārds: ");
            Uzvards = ReadLine() ?? Empty;
            WriteLine("Epasts: ");
            Email = ReadLine() ?? Empty;
            WriteLine("Lietotājvārds: ");
            lietotājs = ReadLine() ?? Empty;
            sec_check:
            WriteLine("Parole: ");
            var line = ReadLine();
            WriteLine("Ievadiet vēlreiz: ");
            var temp = ReadLine();
            if (line == temp)
            {
                Parole = temp;
            }
            else
            {
                WriteLine("Kaut kas nav ok");
                goto sec_check;
            }

            WriteLine("Cik pasūtījumus pievienot? ");
            var pasūtījumi = Convert.ToInt32(ReadLine());
            Pasūtījumu = new Pasūtījums[pasūtījumi];
            for (var i = 0; i < pasūtījumi; i++) Pasūtījumu[i] = new Pasūtījums();
        }

        public Klients(string email, string vards, string uzv, string liet, string pas, Pasūtījums[] p)
        {
            Vārds = vards;
            Uzvards = uzv;
            lietotājs = liet;
            Parole = pas;
            Email = email;
            Pasūtījumu = p;
        }
    }
}