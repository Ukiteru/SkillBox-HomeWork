using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme7HomeWork
{
    struct Diary
    {

        public string Print()
        {
            return $"{this.ID,15} {this.DateAdd,15} {this.Name,15} {this.Text,15} {this.Task,10} {this.TaskTime,10}";
        }
        /// <summary>
        /// Номер записи
        /// </summary>
        public int ID { get { return this.ID; } set { this.ID = value; } }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateAdd { get { return this.DateAdd; } set { this.DateAdd = value; } }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Name { get { return this.Name; } set { this.Name = value; } }

        /// <summary>
        /// Текст записи
        /// </summary>
        public string Text { get { return this.Text; } set { this.Text = value; } }

        /// <summary>
        /// Задания на день
        /// </summary>
        public string Task { get { return this.Task; } set { this.Task = value; } }

        /// <summary>
        /// Время встреч
        /// </summary>
        public DateTime TaskTime { get { return this.TaskTime; } set { this.TaskTime = value; } }
        public Diary(int ID, DateTime DateAdd, string Name, string Text, string Task, DateTime TaskTime)
        {
            this.ID = ID;
            this.DateAdd = new DateTime();
            this.Name = Name;
            this.Text = Text;
            this.Task = Task;
            this.TaskTime = new DateTime();

        }
    }
}
