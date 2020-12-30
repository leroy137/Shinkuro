using Shinkuro.Models;
using System;
using System.Windows;
using Shinkuro.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

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

        private void BringSelectionIntoView(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            if (selector is ListBox)
            {
                (selector as ListBox).ScrollIntoView(selector.SelectedItem);
            }
        }
    }
}
