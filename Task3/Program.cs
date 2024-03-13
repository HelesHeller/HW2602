using System.Threading;
using System;

namespace Task_3
{
    class Program
    {
        static void Main()
        {
            int[] numbers = new int[] { 5, 8, 3, 2, 7, 1, 4, 6 };

            Console.WriteLine("Исходная коллекция:");
            PrintCollection(numbers);

            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(() =>
                {
                    DecreaseValuesInCollection(numbers);
                });
                thread.Start();
                thread.Join();
            }

            Console.WriteLine("Измененная коллекция после выполнения метода трижды:");
            PrintCollection(numbers);

            Console.WriteLine("Работа приложения завершена.");
        }

        static void DecreaseValuesInCollection(int[] collection)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал уменьшение значений в коллекции.");

            for (int i = 0; i < collection.Length; i++)
            {
                Interlocked.Decrement(ref collection[i]);
            }

            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} закончил уменьшение значений в коллекции.");
        }

        static void PrintCollection(int[] collection)
        {
            foreach (var number in collection)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();
        }
    }
}
