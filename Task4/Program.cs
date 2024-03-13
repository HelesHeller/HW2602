using System.Threading.Tasks;
using System.Threading;
using System;

namespace Task_4
{
    class Program
    {
        static void Main()
        {
            int[] numbers = new int[] { 15, 25, 10, 8, 20, 5, 15, 12 };

            Console.WriteLine("Исходная коллекция:");
            PrintCollection(numbers);

            Task[] tasks = new Task[3];
            for (int i = 0; i < 3; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    DecreaseValuesInCollection(numbers);
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Измененная коллекция после выполнения метода трижды:");
            PrintCollection(numbers);

            Console.WriteLine("Работа приложения завершена.");
        }

        static void DecreaseValuesInCollection(int[] collection)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал уменьшение значений в коллекции.");

            for (int i = 0; i < collection.Length; i++)
            {
                Interlocked.Add(ref collection[i], -5);
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
