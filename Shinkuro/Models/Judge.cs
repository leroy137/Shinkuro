using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.Models.Base;

namespace Shinkuro.Models
{
    public class Judge : ModelBase
    {
        private Int32 _number;
        private String _surname;
        private String _name;
        private String _patronymic;
        private String _post;
        private String _city;
        private String _rank;
        private String _info;

        public Int32 Number 
        { 
            get { return _number; } 
            set { Set<Int32>(ref _number, value); } 
        }

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

                Set<String>(ref _surname, value);
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

                Set<String>(ref _name, value);
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

                Set<String>(ref _patronymic, value);
            }
        }

        // Категория
        public String Rank 
        { 
            get { return _rank; }
            set { Set<String>(ref _rank, value); } 
        }

        // должность
        public String Post 
        { 
            get { return _post; } 
            set { Set<String>(ref _post, value); } 
        }

        // город
        public String City 
        { 
            get { return _city; } 
            set { Set<String>(ref _city, value); }
        }

        // дополнительная информация
        public String Info 
        { 
            get { return _info; } 
            set { Set<String>(ref _info, value); } 
        } 

        public Judge()
        {

        }

        public Judge(String surname, String name, String patronymic, String rank, String post, String city, String info)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Rank = rank;
            Post = post;
            City = city;
            Info = info;
        }

        public String FIO => $"{Surname} {Name} {Patronymic}";

        public String ShortFIO => $"{Surname} {Name?.ToUpper()[0] + "."}{Patronymic?.ToUpper()[0] + "."}";
    }
}
