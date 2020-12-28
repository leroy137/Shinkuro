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
    /// Логика взаимодействия для GroupEditorWindow.xaml
    /// </summary>
    public partial class GroupEditorWindow : Window
    {
        public AgeCategory GroupEdit { get; set; }

        public String GroupName { get; set; }

        public String StartYear { get; set; }

        public String EndYear { get; set; }

        public String GroupDescription { get; set; }

        public GroupEditorWindow()
        {
            InitializeComponent();
            windowGroupEditor.DataContext = this;
        }

        public GroupEditorWindow(AgeCategory group) : this()
        {
            GroupName = group.Name;
            StartYear = group?.StartYear.ToString() ?? "";
            EndYear = group?.FinishYear.ToString() ?? "";
            GroupDescription = group.Description;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32? startYear = null;
                Int32? endYear = null;

                if (Int32.TryParse(StartYear, out int sy))
                    startYear = sy;

                if (Int32.TryParse(EndYear, out int ey))
                    endYear = ey;

                AgeCategory group = new AgeCategory(GroupName, startYear, endYear, GroupDescription);
                GroupEdit = group;
                this.DialogResult = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
