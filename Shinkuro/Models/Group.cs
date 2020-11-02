using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    /// <summary>
    /// класс соревнования по фигурам
    /// </summary>
    public class Group
    {

        /// <summary>
        /// Название группы
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Начальный год диапазона участников группы
        /// </summary>
        public Int32? StartYear { get; set; }
        /// <summary>
        /// Конченый год диапазона участников группы
        /// </summary>
        public Int32? FinishYear { get; set; }
        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public String Description { get; set; }
        public List<Figure> Figures { get; set; } = new List<Figure>();
        public List<Judge> Judges { get; set; } = new List<Judge>();
        public List<Patricipant> Patricipants { get; set; } = new List<Patricipant>();


        public Group()
        {

        }

        public Group(String name, Int32? startYear, Int32? finishYear, String description)
        {
            Name = name;
            StartYear = startYear;
            FinishYear = finishYear;
            Description = description;
        }

        public Group(String name, Int32 startYear, Int32 finishYear, String description, List<Figure> figures, List<Judge> judges, List<Patricipant> patricipants) : this(name, startYear, finishYear, description)
        {
            Figures = figures;
            Judges = judges;
            Patricipants = patricipants;
        }
    }
}
