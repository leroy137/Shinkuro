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

        public String CurrentCompetitionURI { get; set; }
        #region Команды
        // закрытие приложения
        public ICommand CloseApplicationCommand { get; private set; } = new CloseApplicationCommand();

        public ICommand OpenFigureManagerCommand { get; private set; } = new OpenFigureManagerCommand();

        public ICommand GoToHomePageCommand { get; set; }
        public ICommand GoToSettingsPageCommand { get; set; }
        public ICommand GoToPatricipantsPageCommand { get; set; }
        public ICommand GoToJudgePageCommand { get; set; }
        public ICommand GoToCompetitionCommandPageCommand { get; set; }
        public ICommand GoToCompetitionFigurePageCommand { get; set; }
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
            GoToHomePageCommand = new RelayCommand(GoToHomePageCommandExecute,GoToHomePageCommandCanExecute);
            GoToSettingsPageCommand = new RelayCommand(GoToSettingsPageCommandExecute,GoToSettingsPageCommandCanExecute);
            GoToPatricipantsPageCommand = new RelayCommand(GoToPatricipantsPageCommandExecute, GoToPatricipantsPageCommandCanExecute);
            GoToJudgePageCommand = new RelayCommand(GoToJudgePageCommandExecute, GoToJudgePageCommandCanExecute);
            GoToCompetitionCommandPageCommand = new RelayCommand(GoToCompetitionCommandPageCommandExecute, GoToCompetitionCommandPageCommandCanExecute);
            GoToCompetitionFigurePageCommand = new RelayCommand(GoToCompetitionFigurePageCommandExecute,GoToCompetitionFigurePageCommandCanExecute);
        }

        private void GoToHomePageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.HomeAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.HomeAlias, HomePageViewModel);
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
                CurrentCompetitionURI = CompetitionPagesResolver.SettingsAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.SettingsAlias, SettingsPageViewModel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToPatricipantsPageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.PatricipantsAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.PatricipantsAlias, SettingsPageViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToJudgePageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.JudgeAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.JudgeAlias, SettingsPageViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToCompetitionCommandPageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.CompetitionCommandAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.CompetitionCommandAlias, SettingsPageViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToCompetitionFigurePageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.CompetitionFigureAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.CompetitionFigureAlias, SettingsPageViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private bool GoToHomePageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.HomeAlias != CurrentCompetitionURI;
        }

        private bool GoToSettingsPageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.SettingsAlias != CurrentCompetitionURI;
        }

        private bool GoToPatricipantsPageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.PatricipantsAlias != CurrentCompetitionURI;
        }

        private bool GoToJudgePageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.JudgeAlias != CurrentCompetitionURI;
        }

        private bool GoToCompetitionCommandPageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.CompetitionCommandAlias != CurrentCompetitionURI;
        }

        private bool GoToCompetitionFigurePageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.CompetitionFigureAlias != CurrentCompetitionURI;
        }

    }
}
