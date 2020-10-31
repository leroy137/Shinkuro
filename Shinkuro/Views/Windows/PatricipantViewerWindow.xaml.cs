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
    /// Логика взаимодействия для PatricipantViewerWindow.xaml
    /// </summary>
    public partial class PatricipantViewerWindow : Window
    {
        public Patricipant Patricipant { get; set; }

        public PatricipantViewerWindow()
        {
            InitializeComponent();
            windowView.DataContext = this;
        }

        public PatricipantViewerWindow(Patricipant patricipant) : this()
        {
            Patricipant = patricipant;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
