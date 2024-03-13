using System.Threading;
using System;
using System.IO;

namespace Task_1
{
    class Program
    {
        static object fileLock = new object();

        static void Main()
        {
            string filePath = "example.txt";

            Thread readerThread = new Thread(() =>
            {
                ReadFileAndCountSentences(filePath);
            });

            Thread modifierThread = new Thread(() =>
            {
                readerThread.Join();
                ModifyFile(filePath);
            });

            readerThread.Start();
            modifierThread.Start();

            readerThread.Join();
            modifierThread.Join();

            Console.WriteLine("Работа приложения завершена.");
        }

        static void ReadFileAndCountSentences(string filePath)
        {
            lock (fileLock)
            {
                try
                {
                    string content = File.ReadAllText(filePath);
                    int sentenceCount = content.Split('.').Length;

                    Console.WriteLine($"Количество предложений в файле: {sentenceCount}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                }
            }
        }

        static void ModifyFile(string filePath)
        {
            lock (fileLock)
            {
                try
                {
                    string content = File.ReadAllText(filePath);
                    content = content.Replace("!", "#");
                    File.WriteAllText(filePath, content);

                    Console.WriteLine("Файл успешно модифицирован.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при модификации файла: {ex.Message}");
                }
            }
        }
    }
}
