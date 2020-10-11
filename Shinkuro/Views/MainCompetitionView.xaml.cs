using System;
using System.Collections.Generic;
using System.Text;
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
using Shinkuro.Services.Navigation;
using Shinkuro.ViewModels.Base;

namespace Shinkuro.Views
{
    /// <summary>
    /// Логика взаимодействия для MainCompetitionView.xaml
    /// </summary>
    public partial class MainCompetitionView : UserControl
    {

        public MainCompetitionView()
        {
            InitializeComponent();
            CompetitionNavigator.Service = MainFrame.NavigationService;
        }
    }
}
