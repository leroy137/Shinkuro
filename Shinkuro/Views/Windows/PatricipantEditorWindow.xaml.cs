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

        internal Patricipant PatricipantEdit { get; set; }

        public String Surname { get; set; }
        public String PatricipantName { get; set; }
        public String Patronymic { get; set; }
        public String City { get; set; }
        public String Rank { get; set; }
        public String YearBirthday { get; set; }
        public String SportSchool { get; set; }

        public PatricipantEditorWindow()
        {
            InitializeComponent();
            windowEditor.DataContext = this;
        }

        public PatricipantEditorWindow(Patricipant patricipant) : this()
        {
            Surname = patricipant.Surname;
            PatricipantName = patricipant.Name;
            Patronymic = patricipant.Patronymic;
            City = patricipant.City;
            Rank = patricipant.Rank;
            YearBirthday = patricipant.Year.ToString();
            SportSchool = patricipant.SportSchool;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!Int32.TryParse(YearBirthday, out int yearbirthday))
                    throw new Exception("Неверно введен год рождения участника");

                Patricipant patricipant = new Patricipant(Surname, PatricipantName, Patronymic, yearbirthday, City, Rank, SportSchool);
                PatricipantEdit = patricipant;
                this.DialogResult = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
