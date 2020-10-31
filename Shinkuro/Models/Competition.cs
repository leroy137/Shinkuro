using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Shinkuro.Models
{
    public class Competition : IEquatable<Competition>
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

        public bool Equals([AllowNull] Competition other)
        {
            if (other == null)
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (this.Name != other.Name)
                return false;

            if (this.StartDate != other.StartDate)
                return false;

            if (this.FinishDate != other.FinishDate)
                return false;

            if (this.Description != other.Description)
                return false;

            if (this.Organizator != other.Organizator)
                return false;

            if (this.Place != other.Place)
                return false;

            if (this.Contacts != other.Contacts)
                return false;

            return true;
        }

        public void UpdateCompetition(Competition competition)
        {
            this.Name = competition.Name;
            this.StartDate = competition.StartDate;
            this.FinishDate = competition.FinishDate;
            this.Description = competition.Description;
            this.Organizator = competition.Organizator;
            this.Place = competition.Place;
            this.Contacts = competition.Contacts;
        }
    }
}
