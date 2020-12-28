using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class FigureAgeCategory : ModelBase
    {
        private int _number;
        private Figure _figure;

        public Int32 Number
        {
            get { return _number; }
            set { Set<Int32>(ref _number, value); }
        }

        public Figure Figure
        {
            get { return _figure; }
            set { Set<Figure>(ref _figure, value); }
        }

        public FigureAgeCategory(int number, Figure figure)
        {
            Number = number;
            Figure = figure;
        }
    }
}
