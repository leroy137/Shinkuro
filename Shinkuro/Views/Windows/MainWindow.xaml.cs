using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shinkuro.ViewModels;
using Shinkuro.Views;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using Shinkuro.Services.Interfaces;

namespace Shinkuro
{

    public enum WindowTypeHandler
    {
        MenuHandler,
        CompetitionWindow
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowHandler
    {
        internal ApplicationCoreContext MainContext { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.MainContext = new ApplicationCoreContext();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowHandler(WindowTypeHandler.MenuHandler);
        }

        public void WindowHandler(WindowTypeHandler windowType)
        {
            switch (windowType) {

                case WindowTypeHandler.CompetitionWindow:
                    MainCompetitionView mainCompetitionView = new MainCompetitionView();
                    mainCompetitionView.DataContext = new MainCompetitionViewModel(MainContext, this);
                    cp.Content = mainCompetitionView;
                    break;
                default:
                    MenuView menuView = new MenuView();
                    menuView.DataContext = new MenuViewModel(MainContext, this);
                    cp.Content = menuView;
                    break;
            }
        }
    }
}
