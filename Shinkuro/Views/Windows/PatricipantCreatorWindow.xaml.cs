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
    /// Логика взаимодействия для PatricipantCreatorWindow.xaml
    /// </summary>
    public partial class PatricipantCreatorWindow : Window
    {
        internal Patricipant PatricipantNew { get; set; }

        public String FirstName{ get; set; }
        public String SecondName { get; set; }
        public String LastName { get; set; }
        public String City { get; set; }
        public String Rank { get; set; }
        public String YearBirthday { get; set; }
        public String Number { get; set; }

        public PatricipantCreatorWindow()
        {
            InitializeComponent();
            windowCreator.DataContext = this;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Ошибка!");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Int32.TryParse(Number, out int number))
                    throw new Exception("Неверно введен номер участника!");

                if (!Int32.TryParse(Number, out int yearbirthday))
                    throw new Exception("Неверно введен год рождения участника");

                Patricipant patricipant = new Patricipant(FirstName, SecondName, LastName, number, yearbirthday, City, Rank);
                PatricipantNew = patricipant;
                this.DialogResult = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Ошибка!");
            }
        }
    }
}
