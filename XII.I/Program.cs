using System;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Ievadiet Divus skaitļus: ");
        double a = Convert.ToDouble(Console.ReadLine());
        double b = Convert.ToDouble(Console.ReadLine());
        Math del = null;
        Console.WriteLine("Izvēlieties opciju: ");
        Console.WriteLine("1) Add \n" +
                          "2) Subtract \n" +
                          "3) Multiply \n" +
                          "4) Divide ");
        del = Console.ReadKey().KeyChar switch
        {
            '1' => new Math((a, b) => Add(a, b)),
            '2' => new Math((a,b) => Subtract(a,b)),
            '3' => new Math((a,b) => Multiply(a,b)),
            '4' => new Math((a,b) => Divide(a,b)),
            _ => null
        };
        Console.ReadLine();
    }
    
    
    
    static double Add(double a, double b) => a + b;
    static double Subtract(double a, double b) => a - b;
    static double Multiply(double a, double b) => a * b;
    static double Divide(double a, double b) => a / b;

    }
public delegate double Math(double a, double b);

