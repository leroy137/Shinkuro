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
    /// Логика взаимодействия для StringValueDialogWindow.xaml
    /// </summary>
    public partial class StringValueDialogWindow : Window
    {

        public string Message { get => txtMessage.Text; set => txtMessage.Text = value; }

        public string Value { get => txtUserValue.Text; set => txtUserValue.Text = value; }

        public StringValueDialogWindow()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(E.Source is Button button)) return;
            DialogResult = !button.IsCancel;
            Close();
        }
    }
}
