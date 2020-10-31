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

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для JudgeCreatorWindow.xaml
    /// </summary>
    public partial class JudgeCreatorWindow : Window
    {
        public JudgeCreatorWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
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
