using System.Threading.Tasks;
using System;

namespace Task_2
{
    class Program
    {
        static object arrayLock = new object();

        static void Main()
        {
            int[] dataArray = { 5, 3, 8, 2, 1, 7, 4, 6 };

            Task sortTask = Task.Run(() =>
            {
                SortArray(dataArray);
            });

            Task searchTask = sortTask.ContinueWith((previousTask) =>
            {
                previousTask.Wait();
                int targetNumber = 4;
                SearchInSortedArray(dataArray, targetNumber);
            });

            searchTask.Wait();

            Console.WriteLine("Работа приложения завершена.");
        }

        static void SortArray(int[] array)
        {
            lock (arrayLock)
            {
                Console.WriteLine("Начало сортировки массива.");
                Array.Sort(array);
                Console.WriteLine("Окончание сортировки массива.");
            }
        }

        static void SearchInSortedArray(int[] sortedArray, int target)
        {
            lock (arrayLock)
            {
                Console.WriteLine($"Ищем число {target} в отсортированном массиве.");
                bool found = Array.BinarySearch(sortedArray, target) >= 0;
                Console.WriteLine($"Число {target} {(found ? "найдено" : "не найдено")} в отсортированном массиве.");
            }
        }
    }
}
