using System;
using System.Windows;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Models;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для FigureCreatorWindow.xaml
    /// </summary>
    public partial class FigureCreatorWindow : Window
    {

        public Figure FigureNew { get; set; }
        public String FigureName { get; set; }
        public String FigureComplexity { get; set; }
        public String FigureDescription { get; set; }
        public ICommand CreateFigureCommand { get; set; }
        public FigureCreatorWindow()
        {
            InitializeComponent();
            windowFigure.DataContext = this;
            CreateFigureCommand = new RelayCommand(CreateFigureCommandExecute, CreateFigureCommandCanExecute);
        }

        private void CreateFigureCommandExecute(object obj)
        {
            try
            {

                if (!Double.TryParse(FigureComplexity, out double complexity))
                    throw new FormatException("Сложность задана некорректно!");

                Figure figure = new Figure(FigureName, complexity, FigureDescription);
                FigureNew = figure;
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool CreateFigureCommandCanExecute(object obj)
        {
            return true;
        }
    }
}
