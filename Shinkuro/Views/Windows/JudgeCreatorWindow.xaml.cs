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
    /// Логика взаимодействия для JudgeCreatorWindow.xaml
    /// </summary>
    public partial class JudgeCreatorWindow : Window
    {
        public Judge JudgeNew { get; set; }
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String LastName { get; set; }
        public String City { get; set; }
        public String Number { get; set; }
        public String Category { get; set; }
        public String Work { get; set; }
        public String Description { get; set; }

        public JudgeCreatorWindow()
        {
            InitializeComponent();
            windowCreatorJudge.DataContext = this;
        }

        public JudgeCreatorWindow(Judge judge) : this()
        {
            FirstName = judge.Firstname;
            SecondName = judge.Surname;
            LastName = judge.Lastname;
            City = judge.City;
            Number = judge.Number.ToString();
            Category = judge.Rank;
            Work = judge.Post;
            Description = judge.Info;
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
                if (!Int32.TryParse(Number, out int number))
                    throw new Exception("Формат поля НОМЕР судьи задан некорректно!");

                Judge judge = new Judge(FirstName, SecondName, LastName, number, Category, Work, City, Description);
                JudgeNew = judge;

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
