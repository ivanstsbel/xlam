using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ConsoleApp1
{


    public struct PointStructDouble
    {
        public double X;
        public double Y;
    }
    public class PointClassDouble
    {
        public double X;
        public double Y;
    }



    internal class lesson3 : Ilesson
    {
        public string name => "3";

        public string description => "Задание 3. Расстояние между точками";

        public void demo()
        {
            task();
        }
        private static double[,] mass;
        int n = 1000;


        public void task()
        {
            Console.WriteLine($"Point|PointStructDouble|PointClassDouble|Ratio");
            for (int k = 1; k <= 5; k++)
            {
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                
                StructMethod();
                startTime.Stop();
                var resultTime = startTime.Elapsed;
                var startTime2 = System.Diagnostics.Stopwatch.StartNew();
                ClassMethod();
                startTime2.Stop();
                var resultTime2 = startTime2.Elapsed;
                Console.WriteLine($" {n}|{resultTime}|{resultTime2}|{resultTime2/ resultTime}");
                n = n + n/k;

            }

        }
        public void StructMethod()
        {
            
            Randoms(n);
            var PointStructDouble_A = new PointStructDouble();
            var PointStructDouble_B = new PointStructDouble();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    PointStructDouble_A.X = mass[i, j];
                    PointStructDouble_A.Y = mass[i + 1, j];
                    PointStructDouble_B.X = mass[i, j + 1];
                    PointStructDouble_B.Y = mass[i + 1, j + 1];
                }
            }



        }
        public void ClassMethod()
        {

            var PointClassDouble_A = new PointClassDouble();
            var PointClassDouble_B = new PointClassDouble();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    PointClassDouble_A.X = mass[i, j];
                    PointClassDouble_A.Y = mass[i + 1, j];
                    PointClassDouble_B.X = mass[i, j + 1];
                    PointClassDouble_B.Y = mass[i + 1, j + 1];
                }
            }



        }
        public static double Randoms(int ValveDot)
        {

            mass = new double[ValveDot+1, ValveDot+1];
            Random rand = new Random();
            for (int i = 0; i <= ValveDot; i++)
            {
                for (int j = 0; j <= ValveDot; j++)
                {
                    mass[i, j] = rand.Next();
                }

            }
            return 0;
        }
        public static double PointDistanceDouble(PointStructDouble pointOne, PointStructDouble pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        public static double PointDistanceDoubleclass(PointClassDouble pointOne, PointClassDouble pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));

        }
        public class BechmarkClass
        {
            public int SumValueType(int value)
            {
                return 9 + value;
            }
            public int SumRefType(object value)
            {
                return 9 + (int)value;
            }
            [Benchmark]
            public void TestSum()
            {
                SumValueType(99);
            }
            [Benchmark]
            public void TestSumBoxing()
            {
                object x = 99;
                SumRefType(x);
            }
        }
    }
}






