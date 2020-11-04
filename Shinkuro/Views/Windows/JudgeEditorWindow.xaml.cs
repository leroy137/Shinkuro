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
    /// Логика взаимодействия для JudgeEditorWindow.xaml
    /// </summary>
    public partial class JudgeEditorWindow : Window
    {

        public Judge JudgeEdit { get; set; }
        public String Surname { get; set; }
        public String JudgeName { get; set; }
        public String Patronymic { get; set; }
        public String City { get; set; }
        public String Category { get; set; }
        public String Work { get; set; }
        public String Description { get; set; }
        public JudgeEditorWindow()
        {
            InitializeComponent();
            windowEditJudge.DataContext = this;
        }

        public JudgeEditorWindow(Judge judge) : this()
        {
            Surname = judge.Surname;
            JudgeName = judge.Name;
            Patronymic = judge.Patronymic;
            City = judge.City;
            Category = judge.Rank;
            Work = judge.Post;
            Description = judge.Info;
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

                Judge judge = new Judge(Surname, JudgeName, Patronymic, Category, Work, City, Description);
                JudgeEdit = judge;

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
