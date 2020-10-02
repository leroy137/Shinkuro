using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    internal class Competition
    {
        string _name;
        DateTime? _startDate;
        DateTime? _finishDate;
        String _description;

        // название соревнования
        public String Name
        {
            get { return _name; }
            set
            {
                if (value.Trim().Length == 0) 
                    throw new ArgumentException("Название соревнования не может быть пустым!");
                _name = value;
            }
        }
        // дата начала соревнований
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        // дата окончания соревнований
        public DateTime? FinishDate
        {
            get { return _finishDate; }
            set { _finishDate = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public String Place { get; set; } // место проведения
        public String Organizator { get; set; } // организатор
        public String Contacts { get; set; } // контакты


        public Competition(String name)
        {
            Name = name;
        }

        public Competition(String name, DateTime? startDate, DateTime? finishDate) : this(name)
        {
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
        }

        public Competition(String name, DateTime? startDate, DateTime? finishDate, String description, String place, String organizator, String contacts) : this(name, startDate, finishDate)
        {
            Description = description;
            Place = place;
            Organizator = organizator;
            Contacts = contacts;
        }
    }
}
