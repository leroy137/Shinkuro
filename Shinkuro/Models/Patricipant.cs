using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class Patricipant
    {
        private int _number;

        private String _firstname;
        private String _surname;
        private String _lastname;

        private int _year;
        private string _city;
        private string _rank;

        /// <summary>
        /// Фамилия
        /// </summary>
        public String Firstname
        {
            get { return _firstname; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception("ФАМИЛИЯ участника не может быть пустым полем!");

                int maxSizeField = 50;
                if (value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ФАМИЛИИ участника в {maxSizeField} символов!");

                _firstname = value;
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public String Surname
        {
            get { return _surname; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception("ИМЯ участника не может быть пустым!");

                int maxSizeField = 50;
                if (value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ИМЯ участника в {maxSizeField} символов!");

                _surname = value;
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public String Lastname
        {
            get { return _lastname; }
            set
            {
                int maxSizeField = 50;
                if (value.Length > maxSizeField)
                    throw new Exception($"Превышен лимит символов для ОТЧЕСТВО участника в {maxSizeField} символов!");

                _lastname = value;
            }
        }

        public Int32 Number
        {
            get { return _number; }
            set
            {
                if (value <= 0)
                    throw new Exception("Номер участника не может быть меньше или равен нулю!");
                _number = value;
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

        public Patricipant()
        {

        }

        public Patricipant(String firstname, String surname, String lastname, int number, int year, string city, string rank)
        {
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            Number = number;
            Year = year;
            City = city;
            Rank = rank;
        }
    }
}
