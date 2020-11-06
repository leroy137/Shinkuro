using System.Windows;
using Shinkuro.ViewModels;


namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для FiguresSeletedWindow.xaml
    /// </summary>
    public partial class FiguresSeletedWindow : Window
    {
        public FiguresSeletedWindow()
        {
            InitializeComponent();
        }

        public FiguresSeletedWindow(FigureSelectedViewModel viewModel) : this()
        {
            window.DataContext = viewModel;
        }
    }
}
