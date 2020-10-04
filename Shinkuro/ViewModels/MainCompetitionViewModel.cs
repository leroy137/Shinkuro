using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using Shinkuro.Services.Navigation;
using System.Windows.Input;
using System.Windows;

namespace Shinkuro.ViewModels
{
    /// <summary>
    /// главная вью модель окна управления текущим соревнованием
    /// </summary>
    internal class MainCompetitionViewModel : ViewModelBase
    {

        #region Команды
        
        // закрытие приложения
        public ICommand CloseApplicationCommand { get; private set; } = new CloseApplicationCommand();

        public ICommand OpenFigureManagerCommand { get; private set; } = new OpenFigureManagerCommand();

        public ICommand GoToHomePageCommand { get; set; }

        public ICommand GoToSettingsPageCommand { get; set; }

        #endregion


        public ViewModelBase HomePageViewModel { get; private set; }
        public ViewModelBase SettingsPageViewModel { get; private set; }


        public MainCompetitionViewModel()
        {
            HomePageViewModel = new HomePageViewModel();
            SettingsPageViewModel = new SettingsPageViewModel();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GoToHomePageCommand = new RelayCommand(GoToHomePageCommandExecute);

            GoToSettingsPageCommand = new RelayCommand(GoToSettingsPageCommandExecute);

        }

        private void GoToHomePageCommandExecute(object viewModel)
        {
            try
            {
                CompetitionNavigator.Navigate("Home", HomePageViewModel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToSettingsPageCommandExecute(object viewModel)
        {
            try
            {
                CompetitionNavigator.Navigate("Settings", SettingsPageViewModel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
}
    }
}
