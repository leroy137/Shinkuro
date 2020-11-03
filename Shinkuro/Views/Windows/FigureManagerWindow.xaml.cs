using Shinkuro.Models;
using System;
using System.Windows;
using Shinkuro.ViewModels;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для FigureManagerWindow.xaml
    /// </summary>
    public partial class FigureManagerWindow : Window
    {

        public FigureManagerWindow()
        {
            InitializeComponent();
            figureManager.DataContext = new FigureManagerViewModel();
        }
    }
}
