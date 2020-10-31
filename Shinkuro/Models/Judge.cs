using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class Judge
    {

        int _number;
        private String _firstname;
        private String _surname;
        private String _lastname;

        public Int32 Number // номер судьи
        {
            get { return _number; }
            set
            {
                if (value <= 0)
                    throw new Exception("Номер судьи не может быть числом равным нулю или меньше него!");
                _number = value;
            }
        }

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

        public String Rank { get; set; } // Категория
        public String Post { get; set; } // должность
        public String City { get; set; } // город
        public String Info { get; set; } // дополнительная информация

        public Judge()
        {

        }

        public Judge(String firstname, String surname, String lastname, Int32 number, String rank, String post, String city, String info)
        {
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            Number = number;
            Rank = rank;
            Post = post;
            City = city;
            Info = info;
        }
    }
}
