using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Sand_Dunes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program uses several user defined parameters to construct the bitmaps. These parameters are D, beta, t and steps.\n");
            Console.Write("Please enter a value for D, this can be any positive floating point number: ");
            double d = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter a value for beta, this can be any positive floating point number: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter a value for t. This is the time the simulation will run for. It must be an integer greater than 0: ");
            int t = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter a value for the number of steps. This is how often a bitmap image will be generated. It must be an integer greater than 0: ");
            int s = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nThe program will apply an algorithm to an array of randomly generated numbers.");
            Console.Write("Please enter the number of rows in the array: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the number of columns in the array: ");
            int cols = Convert.ToInt32(Console.ReadLine());
            SandWave sw = new SandWave(rows, cols, d, t, b, s);//Creates a new object that runs the code with the parameters
            sw.Run();
            Console.WriteLine();
        }
    }
}