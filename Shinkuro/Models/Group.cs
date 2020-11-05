using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.Models.Base;

namespace Shinkuro.Models
{
    /// <summary>
    /// класс соревнования по фигурам
    /// </summary>
    public class Group : ModelBase
    {
        private string _name;
        private int? _startYear;
        private int? _finishYear;
        private string _description;
        /// <summary>
        /// Название группы
        /// </summary>
        public String Name 
        { 
            get { return _name; } 
            set { Set<string>(ref _name, value); }
        }
        /// <summary>
        /// Начальный год диапазона участников группы
        /// </summary>
        public Int32? StartYear 
        { 
            get { return _startYear; }
            set { Set<Int32?>(ref _startYear, value); }
        }
        /// <summary>
        /// Конченый год диапазона участников группы
        /// </summary>
        public Int32? FinishYear 
        {
            get { return _finishYear; } 
            set { Set<Int32?>(ref _finishYear, value); }
        }
        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public String Description 
        { 
            get { return _description; } 
            set { Set<String>(ref _description, value); }
        }
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
