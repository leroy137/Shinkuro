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
    /// Логика взаимодействия для PatricipantEditorWindow.xaml
    /// </summary>
    public partial class PatricipantEditorWindow : Window
    {
        public PatricipantEditorWindow()
        {
            InitializeComponent();
        }

        public PatricipantEditorWindow(Patricipant patricipant) : this()
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
