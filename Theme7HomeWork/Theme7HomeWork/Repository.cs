using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme7HomeWork
{
    struct Repository
    {
        private Diary[] diaries; // Основной массив для хранения данных

        private string path; // путь к файлу с данными

        int index; // текущий элемент для добавления в diaries

        string[] titles; // массив, храняий заголовки полей. используется в PrintDbToConsole

        /// <summary>
        /// Констрктор
        /// </summary>
        /// <param name="Path">Путь в файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 0; // текущая позиция для добавления записи в diaries
            this.titles = new string[0]; // инициализаия массива заголовков   
            this.diaries = new Diary[1]; // инициализаия массива записей дневника.    | изначально предпологаем, что данных нет

            this.Load(); // Загрузка данных
        }


        /// <summary>
        /// Метод увеличения текущего хранилища
        /// </summary>
        /// <param name="Flag">Условие увеличения</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.diaries, this.diaries.Length * 2);
            }
        }

        /// <summary>
        /// Метод добавления сотрудника в хранилище
        /// </summary>
        /// <param name="ConcreteDiaries">Сотрудник</param>
        public void Add(Diary ConcreteDiaries)
        {
            this.Resize(index >= this.diaries.Length);
            this.diaries[index] = ConcreteDiaries;
            this.index++;
        }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split(',');


                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(',');

                    Add(new Diary(int.Parse(args[0]), DateTime.Parse(args[1]), args[2], (args[3]), args[4], DateTime.Parse(args[5])));
                }
            }
        }

        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        /// <param name="Path">Путь к файлу сохранения</param>
        public void Save(string Path)
        {
            string temp = String.Format("{0},{1},{2},{3},{4},{5}",
                                            this.titles[0],
                                            this.titles[1],
                                            this.titles[2],
                                            this.titles[3],
                                            this.titles[4],
                                            this.titles[5]);

            File.AppendAllText(Path, $"{temp}\n");

            for (int i = 0; i < this.index; i++)
            {
                temp = String.Format("{0},{1},{2},{3},{4},{5}",
                                        this.diaries[i].ID,
                                        this.diaries[i].DateAdd,
                                        this.diaries[i].Name,
                                        this.diaries[i].Text,
                                        this.diaries[i].Task,
                                        this.diaries[i].TaskTime);
                File.AppendAllText(Path, $"{temp}\n");
            }
        }

        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{this.titles[0],10} {this.titles[1],10} {this.titles[2],10} {this.titles[3],10} {this.titles[4],10} {this.titles[5],10}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.diaries[i].Print());
            }
        }

        /// <summary>
        /// Количество записей в хранилище
        /// </summary>
        public int Count { get { return this.index; } }


    }
}

