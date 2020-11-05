using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class Patricipant
    {

        private String _name;
        private String _surname;
        private String _patronymic;

        private int _year;
        private string _city;
        private string _rank;

        /// <summary>
        /// Фамилия
        /// </summary>
        public String Surname
        {
            get { return _surname; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception("ФАМИЛИЯ участника не может быть пустым полем!");

                int maxSizeField = 50;
                if (value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ФАМИЛИИ участника в {maxSizeField} символов!");

                _surname = value;
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception("ИМЯ участника не может быть пустым!");

                int maxSizeField = 50;
                if (value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ИМЯ участника в {maxSizeField} символов!");

                _name = value;
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public String Patronymic
        {
            get { return _patronymic; }
            set
            {
                int maxSizeField = 50;
                if (!String.IsNullOrWhiteSpace(value)&&value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ОТЧЕСТВО участника в {maxSizeField} символов!");

                _patronymic = value;
            }
        }

        public Int32 Year
        {
            get { return _year; }
            set
            {
                if (value <= 0)
                    throw new Exception("Вы уверены что год рождения участника до нашей эры?");

                _year = value;
            }
        }
        public String City
        {
            get { return _city; }
            set
            {
                _city = value;
            }
        }
        public String Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
            }
        }

        public String SportSchool { get; set; }

        public String FIO => $"{Surname} {Name} {Patronymic}";

        public Patricipant()
        {

        }

        public Patricipant(String surname, String name, String patronymic, int year, string city, string rank, string sportSchool)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Year = year;
            City = city;
            Rank = rank;
            SportSchool = sportSchool;
        }
    }
}
