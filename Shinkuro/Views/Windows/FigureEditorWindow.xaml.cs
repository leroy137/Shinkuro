using System;
using System.Windows;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Models;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для FigureEditorWindow.xaml
    /// </summary>
    public partial class FigureEditorWindow : Window
    {
        public Figure FigureEdit { get; set; }
        public String FigureName { get; set; }
        public String FigureComplexity { get; set; }
        public String FigureDescription { get; set; }
        public ICommand EditFigureCommand { get; set; }

        public FigureEditorWindow()
        {
            InitializeComponent();
            windowFigure.DataContext = this;
            EditFigureCommand = new RelayCommand(EditFigureCommandExecute, EditFigureCommandCanExecute);
        }

        public FigureEditorWindow(Figure figure) : this()
        {
            FigureName = figure.Name;
            FigureDescription = figure.Description;
            FigureComplexity = figure.Complexity.ToString();
        }

        private void EditFigureCommandExecute(object obj)
        {
            try
            {
                if (!Double.TryParse(FigureComplexity, out double complexity))
                    throw new FormatException("Сложность задана некорректно!");

                Figure figure = new Figure(FigureName, complexity, FigureDescription);
                FigureEdit = figure;
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool EditFigureCommandCanExecute(object obj)
        {
            return true;
        }
    }
}
