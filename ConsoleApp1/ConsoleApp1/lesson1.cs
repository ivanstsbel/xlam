using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class lesson1 : Ilesson
    {
        public string name => "1";

        public string description => "Задание 1.1 Простые числа";

        public void demo()
        {
            task();
        }
        private void task()
        {
            int num;
            while (true)
            {
                Console.WriteLine("Введите число: ");
                string numstr = Console.ReadLine();
                if (!int.TryParse(numstr, out num))
                {
                    Console.WriteLine("Ошибка! Введено не число!");
                    continue;
                }
                else break;
            }
            //Console.WriteLine($"{num}");
            int i = 2, d = 0;
            bool whi = true;



            while (whi)
            {
                if (i < num)
                {
                    if (num % i == 0)
                    {
                        d++;
                        i++;
                    }
                    else i++;
                }
                else if (d == 0)
                {
                    Console.WriteLine($"Число {num} простое");
                    whi = false;
                }
                else if (d != 0)
                {
                    Console.WriteLine($"Число {num} не простое");
                    whi = false;
                }

            }
        }
    }
}
