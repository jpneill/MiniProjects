using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Sand_Dunes
{
    class SandWave
    {
        private int x, i, j, k, l, m, t, dsteps;//x is to generate random numbers for initiation; i, j, k, l and m are all for loops; t is the number of iterations; dsteps is how often the bitmaps are saved
        private double h, D, beta, sum;//h is a random number between -0.05 and 0.05; D and beta are user defined positive constants; sum is used in the algorithm
        private double[,] hmap, delta1, delta2, a, I, delta, UpdatedMap;
        public double[] ainput;
        public double d
        {
            get { return D; }
            set { D = value; }
        }
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }
        public int T
        {
            get { return t; }
            set { t = value; }
        }
        public int DSteps
        {
            get { return dsteps; }
            set { dsteps = value; }
        }
        //This is the constructor. It takes in the size of the array, the constants, the number of iterations and how often a bitmap is saved
        public SandWave(int rows, int cols, double D, int t, double beta, int dsteps)
        {
            hmap = new double[rows, cols];
            delta = new double[rows, cols];
            delta1 = new double[rows, cols];
            delta2 = new double[rows, cols];
            UpdatedMap = new double[rows, cols];
            I = new double[rows, cols];
            a = new double[3, 3];
            ainput = new double[8];
            d = D;
            T = t;
            Beta = beta;
            DSteps = dsteps;
        }
        public void Run()
        {
            Initiate();
            Iterate();
        }
        public void Initiate()//This method sets up the initial state of the map and the 'a' matrix
        {
            //First generate the initial map
            Random rand = new Random();
            for (i = 0; i <= hmap.GetUpperBound(0); i++)
            {
                for (j = 0; j <= hmap.GetUpperBound(1); j++)
                {
                    x = rand.Next(-20, 20);
                    if (x != 0)
                        x /= Math.Abs(x);
                    h = rand.NextDouble() / 20;
                    hmap[i, j] = h * (double)x;
                }
            }
            //Now we ask the user to input values for the a matrix.
            Console.WriteLine("\n\nYou may now input 8 values that represent the strength of the wind.\nThis will be represented in a 3x3 matrix with the center element set to 0.\nEach value must be between 0 and 1.\nThe sum of the values must be equal to 1.\nIf the above condition is not met the program will default to a preset matrix for a.\nThis is how your entries will be fit into the matrix:\n");
            Console.WriteLine("a1   a2  a3");
            Console.WriteLine("a4   *   a5");
            Console.WriteLine("a6   a7  a8");
            Console.WriteLine();
            for (i = 0; i < ainput.Length; i++)
            {
                Console.Write("Please enter entry {0}: ", i + 1);
                ainput[i] = Convert.ToDouble(Console.ReadLine());
            }
            sum = 0;
            for (i = 0; i < 8; i++)//Find the total value of all entries
            {
                sum += ainput[i];
            }
            if (sum != 1)//Check that the sum is equal to 1
            {
                Console.WriteLine("\nYour values sum to a value greater than 1. Switching to default matrix.");
                defaulta();//Sets the matrix a to the default matrix
            }
            else//if the sum is equal to 1 assign the input values to the a matrix
            {
                a[0, 0] = ainput[0];
                a[0, 1] = ainput[1];
                a[0, 2] = ainput[2];
                a[1, 0] = ainput[3];
                a[1, 2] = ainput[4];
                a[2, 0] = ainput[5];
                a[2, 1] = ainput[6];
                a[2, 2] = ainput[7];
                a[1, 1] = 0;
            }
            Console.WriteLine("\nThe a matrix is: ");
            for (i = 0; i < a.GetUpperBound(0); i++)
            {
                for (j = 0; j < a.GetUpperBound(1); j++)
                    Console.Write("{0} ", a[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private void defaulta()
        {
            //This sets up a default a matrix with values summing to 1
            a[1, 1] = 0;
            a[0, 0] = a[2, 0] = 0.143;
            a[0, 1] = a[0, 2] = a[1, 2] = a[2, 1] = a[2, 2] = 0.071;
            a[1, 0] = 0.359;
        }
        private void HeightInc(double[,] update)
        {
            //This method calculates the lattice delta(i,j)
            //First we find delta1(i,j), then we find delta2(i,j). Then we can find I(i,j) = delta1(i,j) + delta2(i,j). Finally we can find delta(i,j)
            //I had originally used four different loops to calculate each part

            //calculate delta1
            //Note! A lot of for loops and if statements are used here due to the presence of periodic boundary conditions. I think I could clean this up a lot using another method and a better syntax for all the if statements, but time is against me. This has been tested and works.
            for (i = 0; i <= update.GetUpperBound(0); i++)
                for (j = 0; j <= update.GetUpperBound(1); j++)
                {
                    sum = 0;
                    if (i == 0 && j == 0)//Find sum for the upper left corner element
                    {
                        sum = a[0, 0] * update[update.GetUpperBound(0), update.GetUpperBound(1)] + a[0, 1] * update[update.GetUpperBound(0), j] + a[0, 2] * update[update.GetUpperBound(0), j + 1] + a[1, 0] * update[i, update.GetUpperBound(1)] + a[1, 2] * update[i, j + 1] + a[2, 0] * update[i + 1, update.GetUpperBound(1)] + a[2, 1] * update[i + 1, j] + a[2, 2] * update[i + 1, j + 1];
                    }
                    else if (i == update.GetUpperBound(0) && j == update.GetUpperBound(1))//Find sum for the bottom right corner element
                    {
                        sum = a[0, 0] * update[i - 1, j - 1] + a[0, 1] * update[i - 1, update.GetUpperBound(1)] + a[0, 2] * update[i - 1, 0] + a[1, 0] * update[i, j - 1] + a[1, 2] * update[i, 0] + a[2, 0] * update[0, j - 1] + a[2, 1] * update[0, j] + a[2, 2] * update[0, 0];
                    }
                    else if (i == update.GetUpperBound(0) && j == 0)//Find sum for the bottom left corner element
                    {
                        sum = a[0, 0] * update[i - 1, update.GetUpperBound(1)] + a[0, 1] * update[i - 1, j] + a[0, 2] * update[i - 1, j + 1] + a[1, 0] * update[i, update.GetUpperBound(1)] + a[1, 2] * update[i, j + 1] + a[2, 0] * update[0, update.GetUpperBound(1)] + a[2, 1] * update[0, 0] + a[2, 2] * update[0, 1];
                    }
                    else if (i == 0 && j == update.GetUpperBound(1))//Find sum for the top right corner element
                    {
                        sum = a[0, 0] * update[update.GetUpperBound(0), j - 1] + a[0, 1] * update[update.GetUpperBound(0), update.GetUpperBound(1)] + a[0, 2] * update[update.GetUpperBound(0), 0] + a[1, 0] * update[i, j - 1] + a[1, 2] * update[0, 0] + a[2, 0] * update[i + 1, j - 1] + a[2, 1] * update[i + 1, j] + a[2, 2] * update[i + 1, 0];
                    }
                    else if (i == 0 && j > 0 && j < update.GetUpperBound(1))//Find sum for the first row excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (k == 0)
                                    sum += a[k, l] * update[update.GetUpperBound(1), j + (l - 1)];
                                else
                                    sum += a[k, l] * update[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (i == update.GetUpperBound(0) && j > 0 && j < update.GetUpperBound(1))//Find sum for the last row excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (k == 2)
                                    sum += a[k, l] * update[0, j + (l - 1)];
                                else
                                    sum += a[k, l] * update[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (j == 0 && i > 0 && i < update.GetUpperBound(0))//Find sum for the first column excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (l == 0)
                                    sum += a[k, l] * update[i + (k - 1), update.GetUpperBound(1)];
                                else
                                    sum += a[k, l] * update[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (j == update.GetUpperBound(1) && i > 0 && i < update.GetUpperBound(0))//Find sum for the last column excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (l == 2)
                                    sum += a[k, l] * update[i + (k - 1), 0];
                                else
                                    sum += a[k, l] * update[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else//Find sum for every element inside the edges where a(i,j) does not take into account the boundaries
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                                sum += (a[k, l] * update[i + (k - 1), j + (l - 1)]);
                    }
                    delta1[i, j] = D * (sum - update[i, j]); //This line calculates the lattice element delta1(i,j) = D((sum of a(k,l)*h(i+k,j+l))-h(i,j))
                    delta2[i, j] = beta * Math.Tanh(update[i, j]) - update[i, j];//This line calculates the lattice element delta2(i,j) = beta*tanh(h(i,j))-h(i,j)
                    I[i, j] = delta1[i, j] + delta2[i, j];//This line calculates the lattice element I(i,j) = delta1(i,j) + delta2(i,j)
                    sum = 0;//finally calculate the lattice delta(i,j)
                    //Again we have the exact same if ladder as above, but with the new matrix I
                    if (i == 0 && j == 0)
                    {
                        sum = a[0, 0] * I[I.GetUpperBound(0), I.GetUpperBound(1)] + a[0, 1] * I[I.GetUpperBound(0), j] + a[0, 2] * I[I.GetUpperBound(0), j + 1] + a[1, 0] * I[i, I.GetUpperBound(1)] + a[1, 2] * I[i, j + 1] + a[2, 0] * I[i + 1, I.GetUpperBound(1)] + a[2, 1] * I[i + 1, j] + a[2, 2] * I[i + 1, j + 1];
                    }
                    else if (i == I.GetUpperBound(0) && j == I.GetUpperBound(1))
                    {
                        sum = a[0, 0] * I[i - 1, j - 1] + a[0, 1] * I[i - 1, I.GetUpperBound(1)] + a[0, 2] * I[i - 1, 0] + a[1, 0] * I[i, j - 1] + a[1, 2] * I[i, 0] + a[2, 0] * I[0, j - 1] + a[2, 1] * I[0, j] + a[2, 2] * I[0, 0];
                    }
                    else if (i == I.GetUpperBound(0) && j == 0)
                    {
                        sum = a[0, 0] * I[i - 1, I.GetUpperBound(1)] + a[0, 1] * I[i - 1, j] + a[0, 2] * I[i - 1, j + 1] + a[1, 0] * I[i, I.GetUpperBound(1)] + a[1, 2] * I[i, j + 1] + a[2, 0] * I[0, I.GetUpperBound(1)] + a[2, 1] * I[0, 0] + a[2, 2] * I[0, 1];
                    }
                    else if (i == 0 && j == I.GetUpperBound(1))
                    {
                        sum = a[0, 0] * I[I.GetUpperBound(0), j - 1] + a[0, 1] * I[I.GetUpperBound(0), I.GetUpperBound(1)] + a[0, 2] * I[I.GetUpperBound(0), 0] + a[1, 0] * I[i, j - 1] + a[1, 2] * I[0, 0] + a[2, 0] * I[i + 1, j - 1] + a[2, 1] * I[i + 1, j] + a[2, 2] * I[i + 1, 0];
                    }
                    else if (i == 0 && j > 0 && j < I.GetUpperBound(1))//Find sum for the first row excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (k == 0)
                                    sum += a[k, l] * I[I.GetUpperBound(1), j + (l - 1)];
                                else
                                    sum += a[k, l] * I[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (i == I.GetUpperBound(0) && j > 0 && j < I.GetUpperBound(1))//Find sum for the last row excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (k == 2)
                                    sum += a[k, l] * I[0, j + (l - 1)];
                                else
                                    sum += a[k, l] * I[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (j == 0 && i > 0 && i < I.GetUpperBound(0))//Find sum for the first column excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (l == 0)
                                    sum += a[k, l] * I[i + (k - 1), I.GetUpperBound(1)];
                                else
                                    sum += a[k, l] * I[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else if (j == I.GetUpperBound(1) && i > 0 && i < I.GetUpperBound(0))//Find sum for the last column excluding the corner elements
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                            {
                                if (l == 2)
                                    sum += a[k, l] * I[i + (k - 1), 0];
                                else
                                    sum += a[k, l] * I[i + (k - 1), j + (l - 1)];
                            }
                    }
                    else
                    {
                        for (k = 0; k <= a.GetUpperBound(0); k++)
                            for (l = 0; l <= a.GetUpperBound(1); l++)
                                sum += (a[k, l] * I[i + (k - 1), j + (l - 1)]);
                    }

                    delta[i, j] = I[i, j] - sum;
                }
        }
        private void Iterate()//This method runs the code for the number of iterations specified by the user
        {
            for (m = 0; m <= t; m++)
            {
                if (m == 0)//this is to create a new matrix derived from hmap that is updated with each iteration. This conserves the original values in hmap.
                {
                    Draw(hmap); // create the initial map
                    HeightInc(hmap);
                    for (i = 0; i <= UpdatedMap.GetUpperBound(0); i++)
                        for (j = 0; j <= UpdatedMap.GetUpperBound(1); j++)
                            UpdatedMap[i, j] = hmap[i, j] + delta[i, j];//create the new map after running through the algorithm
                }
                else
                {
                    HeightInc(UpdatedMap);
                    for (i = 0; i <= UpdatedMap.GetUpperBound(0); i++)
                        for (j = 0; j <= UpdatedMap.GetUpperBound(1); j++)
                            UpdatedMap[i, j] += delta[i, j];
                    if ((m % DSteps) == 0 || m == t)//print out a bitmap if the conditions the user input are met
                        Draw(UpdatedMap);
                }
            }
        }
        private void Draw(double[,] lattice)//A simple save bitmap method.
        {
            Bitmap b = new Bitmap(lattice.GetUpperBound(1), lattice.GetUpperBound(0));
            for (i = 0; i < lattice.GetUpperBound(0); i++)
                for (j = 0; j < lattice.GetUpperBound(1); j++)
                    if (lattice[i, j] < 0.005)//This makes regions below the threshold white
                        b.SetPixel(j, i, Color.White);
                    else//All other regions go from light grey towards black at the highest
                        b.SetPixel(j, i, Color.FromArgb((int)Math.Min(255, Math.Abs(lattice[i, j] * 100)), (int)Math.Min(255, Math.Abs(lattice[i, j] * 100)), (int)Math.Min(255, Math.Abs(lattice[i, j] * 100))));
            if (!Directory.Exists(@"C:\Sand Waves"))
                Directory.CreateDirectory(@"C:\Sand Waves");

            b.Save(String.Format("c:\\Sand Waves\\Time = {0}, beta = {1}, D = {2}.bmp", m, beta, D));
        }
    }
}
