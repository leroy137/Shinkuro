using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Shinkuro.Models
{
    public class SelectedFigure : IComparable<SelectedFigure>
    {
        public Boolean IsSelected { get; set; }
        public Figure Figure { get; set; }

        public SelectedFigure()
        {

        }

        public SelectedFigure(bool isSelected, Figure figure)
        {
            IsSelected = isSelected;
            Figure = figure;
        }

        public int CompareTo([AllowNull] SelectedFigure other)
        {
            if (other == null)
                return -1;

            if (other.Figure == null)
                return -1;

            return this.Figure.Name.CompareTo(other.Figure.Name);
        }
    }
}
