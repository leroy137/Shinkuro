using System.Windows;
using System;
using Shinkuro.Models;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для FigureViewerWindow.xaml
    /// </summary>
    public partial class FigureViewerWindow : Window
    {
        public String FigureName { get; set; }
        public String FigureComplexity { get; set; }
        public String FigureDescription { get; set; }

        public FigureViewerWindow()
        {
            InitializeComponent();
            windowFigure.DataContext = this;
        }

        public FigureViewerWindow(Figure figure) : this()
        {
            FigureName = figure.Name;
            FigureDescription = figure.Description;
            FigureComplexity = figure.Complexity.ToString();
        }
    }
}
