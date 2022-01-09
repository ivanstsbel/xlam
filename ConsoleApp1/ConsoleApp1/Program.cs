using Baseline.ImTools;
using ImTools;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    
    internal class Program
    {
        static List<Ilesson> leassons = new List<Ilesson>()
        {
            new lesson1(), new lesson2(), new lesson3(), new lesson4()
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Для запуска задания введите его немер: ");
            foreach(Ilesson lesson in leassons)
            {
                Console.WriteLine($"Номер: {lesson.name} | {lesson.description}");
            }
            string les = Console.ReadLine();
            foreach (Ilesson lesson in leassons)
            {
                if (les == lesson.name) lesson.demo();
            }

        }
    }
}
