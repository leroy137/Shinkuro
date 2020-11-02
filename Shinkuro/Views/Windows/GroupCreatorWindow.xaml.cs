using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Shinkuro.Models;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для GroupCreatorWindow.xaml
    /// </summary>
    public partial class GroupCreatorWindow : Window
    {
        public Group GroupNew { get; set; }

        public String GroupName { get; set; }

        public String StartYear { get; set; }

        public String EndYear { get; set; }

        public String GroupDescription { get; set; }

        public GroupCreatorWindow()
        {
            InitializeComponent();
            windowGroupCreator.DataContext = this;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32? startYear = null;
                Int32? endYear = null;

                if (Int32.TryParse(StartYear, out int sy))
                    startYear = sy;

                if (Int32.TryParse(EndYear, out int ey))
                    endYear = ey;

                Group group = new Group(GroupName, startYear, endYear, GroupDescription);
                GroupNew = group;
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
