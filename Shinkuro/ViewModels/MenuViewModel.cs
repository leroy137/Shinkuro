using Shinkuro.ViewModels.Base;
using System;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using Shinkuro.Services.Navigation;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Shinkuro.Models;
using Shinkuro.Views.Windows;
using Shinkuro.Services.Interfaces;

namespace Shinkuro.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private String _title = "Shinkuro";

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public String Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
            }
        }
        public ObservableCollection<Competition> Competitions { get; set; }

        public ICommand CreateCompetitionCommand { get; private set; }
        public ICommand OpenCompetitionCommand { get; private set; }

        public ApplicationCoreContext MainContext { get; set; }
        public IWindowHandler Owner { get; set; }
        public MenuViewModel()
        {
            CreateCompetitionCommand = new RelayCommand(CreateCompetitionCommandExecute, CreateCompetitionCommandCanExecute);
            OpenCompetitionCommand = new RelayCommand(OpenCompetitionCommandExecute, OpenCompetitionCommandCanExecute);
        }

        public MenuViewModel(ApplicationCoreContext context) : this()
        {
            MainContext = context;
        }

        public MenuViewModel(ApplicationCoreContext context, IWindowHandler owner) : this(context)
        {
            Owner = owner;
        }

        private bool CreateCompetitionCommandCanExecute(object obj)
        {
            return true;
        }
        private void CreateCompetitionCommandExecute(object obj)
        {
            try
            {
                CreateCompetitionWindow createCompetitionWindow = new CreateCompetitionWindow();
                createCompetitionWindow.ShowDialog();
                if(createCompetitionWindow.DialogResult == true)
                {
                    MainContext.CurrentCompetition = createCompetitionWindow.Competition;
                    Owner.WindowHandler(WindowTypeHandler.CompetitionWindow);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private bool OpenCompetitionCommandCanExecute(object obj)
        {
            return true;
        }
        private void OpenCompetitionCommandExecute(object obj)
        {
            try
            {
                OpenCompetitionWindow openCompetition = new OpenCompetitionWindow();
                openCompetition.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
    }
}
