#nullable enable
using System;
using System.Linq;
using static System.Int32;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            int[,] array = new int[20,20];
            var rnd = new Random();
            var low = MaxValue;
            Tuple<int, int>? cartesianCoordinatesLow = null;
            Tuple<int, int>? cartesianCoordinatesHigh = null;
            var high = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    array[i,j] = rnd.Next(0,99);
                    //Add to a 2D (x,y) array.
                    //limit the array to two numbers, so it doesn't create a mess.
                    Console.Write(" {0} ",array[i,j]);
                    if (array[i,j] <10)
                        //add a space if there's only one number
                        Console.Write(" ");

                    if (array[i, j] < low)
                    {
                        low = array[i,j];
                        cartesianCoordinatesLow = new Tuple<int, int>(i, j);
                    }

                    if (array[i, j] <= high) continue;
                    high = array[i, j];
                    cartesianCoordinatesHigh = new Tuple<int, int>(i, j);

                }

                Console.WriteLine();
            }

            if (cartesianCoordinatesHigh == null || cartesianCoordinatesLow == null) throw new Exception( "Shouldn't happen");
            
            Console.WriteLine(
                "Index params: \n\t Highest number: {0} (coordinates: {1},{2}) \n\t Lowest number: {3} (coordinates: {4},{5})",
                high,
                cartesianCoordinatesHigh.Item1,
                cartesianCoordinatesHigh.Item2,
                low,cartesianCoordinatesLow.Item1,
                cartesianCoordinatesLow.Item2);
            //Sort the array by using LIST. Easy way out.
            //Add 3x /n for clarity
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var temp = array.Cast<int>().ToList();
            temp.Sort();
            var a = 0;
            foreach (var val in temp)
            {
                if (a  == 20)
                {
                    Console.WriteLine();
                    a = 0;
                }
                a++;
                Console.Write(" {0} ",val);
                if (val<10)
                    Console.Write(" ");
            }
            Console.ReadLine();
        } 
    }
}