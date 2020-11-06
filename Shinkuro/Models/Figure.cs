using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.Models.Base;

namespace Shinkuro.Models
{
    public class Figure : ModelBase
    {
        private String _name;
        private double _compexity;
        private String _description;

        public String Name // название фигуры
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new Exception("Название фигуры не должно быть пустым!");

                int maxSizeCount = 100;
                if (value.Length > maxSizeCount)
                    throw new Exception($"Название фигуры превышает максимально допустимую длину в {maxSizeCount} символов!");

                Set<String>(ref _name, value);
            }
        }

        public Double Complexity // сложность
        {
            get { return _compexity; }
            set
            {
                if (value <= 0)
                    throw new Exception("Коэффициент сложности не может быть меньше или равен нулю!");

                Set<double>(ref _compexity, value);
            }
        }

        public String Description
        { 
            get { return _description; }
            set
            {
                Set<String>(ref _description, value);
            }
        }


        public Figure()
        {

        }

        public Figure(String name, Double complexity)
        {
            Name = name;
            Complexity = complexity;
        }

        public Figure(String name, Double complexity, String description) : this(name, complexity)
        {
            Description = description;
        }
    }
}
