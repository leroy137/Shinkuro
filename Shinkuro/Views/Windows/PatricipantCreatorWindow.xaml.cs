﻿using System;
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

        public String Surname { get; set; }
        public String PatricipantName { get; set; }
        public String Patronymic { get; set; }
        public String City { get; set; }
        public String Rank { get; set; }
        public String YearBirthday { get; set; }
        public String SportSchool { get; set; }

        public String Coach { get; set; }

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
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!Int32.TryParse(YearBirthday, out int yearbirthday))
                    throw new Exception("Неверно введен год рождения участника");

                Patricipant patricipant = new Patricipant(Surname, PatricipantName, Patronymic, yearbirthday, City, Rank, SportSchool, Coach);
                PatricipantNew = patricipant;
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
