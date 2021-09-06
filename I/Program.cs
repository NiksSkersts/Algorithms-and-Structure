using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace I
{
    class Program
    {
        static void Main(string[] args)
        {
            Tehnika tehnika1 = InitNewPhone();
            using var reader = new StreamWriter(@"/home/niks-skersts/someText.txt",false);
            reader.WriteLine("Ražotājs: " + tehnika1.Ražotājs.Nosaukums);
            reader.WriteLine("Krāsa: " + tehnika1.Krāsa);
            reader.WriteLine("Modelis: " +tehnika1.Viedierīce.MobilePhone.Model);
            reader.WriteLine("OS: "+tehnika1.Viedierīce.Os);
            reader.WriteLine("Svars: "+tehnika1.Viedierīce.MobilePhone.Weight);
            reader.WriteLine("Sim Kartes: ");
            foreach (var sim in tehnika1.Viedierīce.MobilePhone.Sims)
            {
                reader.WriteLine("\t"+sim.Type);
                reader.WriteLine("\t"+sim.Number);
            }
            reader.Close();
            var to_read = new StreamReader(@"/home/niks-skersts/someText.txt");
            Console.WriteLine(to_read.ReadToEnd());
            to_read.Close();
        }

         static Tehnika InitNewPhone()
         {
             Tehnika temp = new Tehnika();
             // jo taisnība
             Console.WriteLine("Izvēlieties ražotāju:");
             Console.WriteLine("1."+Ražotājs.Valsts.Latvija);
             Console.WriteLine("2."+Ražotājs.Valsts.Igaunija);
             Console.WriteLine("3."+Ražotājs.Valsts.China);
             Console.WriteLine("4."+Ražotājs.Valsts.ASV);
             temp.Ražotājs.Nosaukums = (Ražotājs.Valsts) Convert.ToInt32(Console.ReadLine());
             temp.Viedierīce = new Viedierīce();
                 Console.WriteLine("Ievadiet viedtālruņa OS:");
                 temp.Viedierīce.Os = Console.ReadLine();
                 Console.WriteLine("Ievadiet ekrāna izmēru:");
                 temp.Viedierīce.ScreenSize = Convert.ToDouble(Console.ReadLine());
                 Console.WriteLine("Ir HDMI?");
                 temp.Viedierīce.HasHdmi = Console.ReadLine() == "true";
                 Console.WriteLine("Modeļa nosaukums:");
                 temp.Viedierīce.MobilePhone = new Mobile_Phone();
                 temp.Viedierīce.MobilePhone.Model = Console.ReadLine();
                 Console.WriteLine("Ievadiet svaru:");
                 temp.Viedierīce.MobilePhone.Weight = Convert.ToDouble(Console.ReadLine());
                 Console.WriteLine("cik Sim Kartes ir viedtālrunī?");
                 var simk = Convert.ToInt32(Console.ReadLine());
                 for (int j = 0; j < simk; j++)
                 {
                     Sim s = new Sim();
                     Console.WriteLine("Simkartei ir Nano tips?");
                     if (!Convert.ToBoolean(Convert.ToInt32(Console.ReadLine())))
                     {
                         s.Type = Console.ReadLine();
                     }
                     Console.WriteLine("Iekļauts NR?");
                     if (!Convert.ToBoolean(Convert.ToInt32(Console.ReadLine())))
                     {
                         Console.WriteLine("Ierakstiet NR:");
                         s.Number = Convert.ToInt32(Console.ReadLine());
                     };
                     temp.Viedierīce.MobilePhone.Sims.Add(s);
                 }
                 return temp;
         }
    }

    class Viedierīce
    {
        public string Os { get; set; }
        public double ScreenSize { get; set; }
        public bool HasHdmi { get; set; }
        public Mobile_Phone MobilePhone { get; set; } = null;
    }

    class Mobile_Phone
    {
        public string Model { get; set; }
        public double Weight { get; set; }
        public List<Sim> Sims = new();
    }

    class Sim
    {
        private static Random Random = new();
        public string Type { get; set; } = "Nano";
        public int Number { get; set; } = Random.Next( 00000000,99999999);
    }

    class Tehnika
    {
        public string Krāsa { get; set; }
        public Viedierīce Viedierīce { get; set; } = null;
        public Ražotājs Ražotājs = new Ražotājs();
    }
    class Ražotājs
    {
        public Valsts Nosaukums { get; set; }
        
        public enum Valsts
        {
            Latvija,
            Igaunija,
            China,
            ASV
        }
    }
}