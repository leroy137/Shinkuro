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
using System.Windows.Shapes;
using Shinkuro.Models;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для GroupViewerWindow.xaml
    /// </summary>
    public partial class GroupViewerWindow : Window
    {

        public String GroupName { get; set; }

        public String StartYear { get; set; }

        public String EndYear { get; set; }

        public String GroupDescription { get; set; }

        public GroupViewerWindow()
        {
            InitializeComponent();
            windowGroupViewer.DataContext = this;
        }

        public GroupViewerWindow(Group group) : this()
        {
            GroupName = group.Name;
            StartYear = group?.StartYear.ToString() ?? "";
            EndYear = group?.FinishYear.ToString() ?? "";
            GroupDescription = group.Description;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
