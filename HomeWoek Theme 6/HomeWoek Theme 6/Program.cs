using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace HomeWoek_Theme_6
{
    class Program
    {

        public static int[][] GroupsNumbersArr(uint number)
        {

            /// Если переданное число ноль, то возвращается пустой список групп
            if (number == 0)
                return Array.Empty<int[]>();

            /// Если переданное число единица, то возвращается список групп с одной группой - единицей
            if (number == 1)
                return new int[][] { new int[] { 1 } };

            /// Создание массива для групп
            int[][] groups = new int[(int)Math.Log(number, 2) + 1][];
            groups[0] = new int[] { 1 };
            int indexGroup = 1; // Индекс добавляемой группы

            /// Создание массива чисел содержащего все числа от 1 до заданного
            /// Единица используется как маркер
            /// Вместо удаления элеменов их значение будет приравниваться нулю
            /// После сортировки 1 будет разделять удалённые элементы и оставшиеся
            int[] numbers = new int[number];
            for (int i = 0; i < number; i++)
                numbers[i] = i + 1;

            /// Массив с промежуточными данными
            int[] group = new int[number];

            /// Цикл пока в массиве индекс единицы не последений
            int index1;
            while ((index1 = Array.BinarySearch(numbers, 1)) != number - 1) /// Проверка индекса единицы
            {
                /// Копия элементов в массив группы
                Array.Copy(numbers, group, number);

                int countGroup = 0; /// Количество элементов в группе
                                    /// Перебор элементов группы. i - индекс проверяемого элемента
                for (int i = index1 + 1; i < number; i++)
                {
                    if (group[i] != 0) /// Пропуск удалённых элементов
                    {
                        /// Удаление из группы всех элементов кратных проверяемому, кроме его самого
                        for (int j = i + 1; j < number; j++)
                            if (group[j] % group[i] == 0)
                                group[j] = 0;

                        /// Удаление элемента из массива чисел
                        numbers[i] = 0;
                        /// Счётчик группы увеличивется
                        countGroup++;
                    }

                }
                /// Сортировка массивов после удаления элементов
                Array.Sort(group);
                Array.Sort(numbers);

                /// Создание массива для добавления в группы
                /// и копирование в него значений старше 1
                int[] _gr = new int[countGroup];
                Array.Copy(group, Array.BinarySearch(group, 1) + 1, _gr, 0, countGroup);

                /// Добавление группы в массив групп
                groups[indexGroup] = _gr;
                indexGroup++;

            }
            /// Возврат списка групп
            return groups;
        }

        public static void PrintValues(int[][] myArr)
        {
            var m = myArr.GetLength(0);
            Console.WriteLine($"Значение M равно {m}");

        }
        public static void SaveValues(int[][] myArr)
        {
            StreamWriter sw = new StreamWriter("Result.csv", false, Encoding.Unicode);
            for (int i = 1; i <= myArr.GetLength(0); i++)
            {
                sw.Write("Группа {0}", i);
                for (int j = 0; j < myArr[i - 1].GetLength(0); j++)
                {
                    sw.Write("\t{0}", myArr[i - 1][j]);
                }
                sw.WriteLine();
            }
            sw.Close();
        }
        public static void Archiever(int[][] myArr)
        {
            string source = "Result.csv";
            string compressed = "Result.zip";
            using (FileStream ss = new FileStream(source, FileMode.OpenOrCreate))
            {
                using (FileStream ds = File.Create(compressed))   // поток для записи сжатого файла
                {
                    // поток архивации
                    using (GZipStream cs = new GZipStream(ds, CompressionMode.Compress))
                    {
                        ss.CopyTo(cs); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
                                          source,
                                          ss.Length,
                                          ds.Length);
                    }
                }
            }
        }
        
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string text = File.ReadAllText(@"D:\SkillBox HomeWork\Theme 6 homework\Num.txt");
            uint num = uint.Parse(text);
            int[][] mass = GroupsNumbersArr(num);
            PrintValues(mass);
            SaveValues(mass);
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00} hours:{1:00} minutes:{2:00} seconds.{3:00} milliseconds",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            stopWatch.Stop();
            Console.WriteLine("Если хотите создать архив файла нажмите д/н");
            Console.WriteLine();
            if (Console.ReadKey().Key == ConsoleKey.L) Archiever(mass);
        }
    }
}
