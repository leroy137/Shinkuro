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
    /// Логика взаимодействия для CreateCompetitionWindow.xaml
    /// </summary>
    public partial class CreateCompetitionWindow : Window
    {

        internal Competition Competition { get; set; } = null;

        public CreateCompetitionWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String name = txtCompetitionName.Text;
                DateTime? startDate = dpCompetitionStart.SelectedDate;
                DateTime? finishDate = dpCompetitionFinish.SelectedDate;
                String place = txtPlace.Text;
                String description = txtDescription.Text;
                String contacts = txtContacts.Text;
                String organizator = txtOrganizator.Text;

                Competition competition = new Competition(name, startDate, finishDate, description, place, organizator, contacts);
                Competition = competition;
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
