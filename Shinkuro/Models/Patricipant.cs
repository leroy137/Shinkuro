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

        private static Random rand = new Random();

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

        public static Patricipant CreateRandom()
        {

            List<String> cities = new List<string> { "Оренбург", "Самара", "Салават", "Набережные Челны", "Уфа", "Казань" };
            List<String> names = new List<string> { "Иван", "Алексей", "Петр", "Николай" };
            List<String> surnames = new List<string> { "Иванов", "Алексеев", "Петров", "Николаев" };
            List<String> patronimics = new List<string> { "Иванович", "Алексеевич", "Петрович", "Николаевич" };
            Int32 startYear = 2000;
            Int32 lastYear = 2010;
            List<String> sportSchools = new List<string> { "Спортшкола им. Ивана", "Пингвин", "Газовик", "Алмаз", "Дельфин" };
            List<String> ranks = new List<string> { "КМС", "МС", "ММ", "1 юношеский", "1 взрослый" };

            String surname = surnames[rand.Next(surnames.Count)];
            String name = names[rand.Next(names.Count)];
            String patronimic = patronimics[rand.Next(patronimics.Count)];
            String city = cities[rand.Next(cities.Count)];
            Int32 year = rand.Next(startYear, lastYear + 1);
            String sportSchool = sportSchools[rand.Next(sportSchools.Count)];
            String rank = ranks[rand.Next(ranks.Count)];

            return new Patricipant(surname, name, patronimic, year, city, rank, sportSchool);
        }
    }
}
