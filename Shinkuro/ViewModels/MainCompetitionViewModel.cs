using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using Shinkuro.Services.Navigation;
using System.Windows.Input;
using System.Windows;
using Shinkuro.Models;
using Shinkuro.Services.Interfaces;

namespace Shinkuro.ViewModels
{
    /// <summary>
    /// главная вью модель окна управления текущим соревнованием
    /// </summary>
    internal class MainCompetitionViewModel : ViewModelBase
    {

        public String CurrentCompetitionURI { get; set; }

        public ApplicationCoreContext MainContext { get; set; }
        public Window Owner { get; set; }

        public Competition Competition { get; set; }

        #region Команды
        // закрытие приложения

        public ICommand CloseApplicationCommand { get; private set; } = new CloseApplicationCommand();
        public ICommand OpenFigureManagerCommand { get; private set; } = new OpenFigureManagerCommand();
        public ICommand GoToHomePageCommand { get; set; }
        public ICommand GoToSettingsPageCommand { get; set; }
        public ICommand GoToGroupsPageCommand { get; set; }
        public ICommand GoToPatricipantsPageCommand { get; set; }
        public ICommand GoToJudgePageCommand { get; set; }
        public ICommand GoToCompetitionCommandPageCommand { get; set; }
        public ICommand GoToCompetitionFigurePageCommand { get; set; }

        #endregion

        public ViewModelBase HomePageViewModel { get; private set; }
        public ViewModelBase PatricipantsPageViewModel { get; private set; }
        public ViewModelBase JudgePageViewModel { get; private set; }        
        public ViewModelBase GroupsPageViewModel { get; private set; }
        public ViewModelBase CompetitionFigurePageViewModel { get; private set; }
        public ViewModelBase CompetitionCommandPageViewModel { get; private set; }
        public ViewModelBase SettingsPageViewModel { get; private set; }

        public MainCompetitionViewModel(ApplicationCoreContext context, Window owner)
        {
            Owner = owner;
            MainContext = context;
            Competition = context.CurrentCompetition;
            owner.WindowState = WindowState.Maximized;
            InitializeModelViews();
            InitializeCommands();
        }

        private void InitializeModelViews()
        {
            HomePageViewModel = new HomePageViewModel();
            SettingsPageViewModel = new SettingsPageViewModel(Competition);
            JudgePageViewModel = new JudgePageViewModel(MainContext);
            GroupsPageViewModel = new GroupsPageViewModel();
            CompetitionCommandPageViewModel = new CompetitionCommandPageViewModel();
            CompetitionFigurePageViewModel = new CompetitionFigurePageViewModel();
            PatricipantsPageViewModel = new PatricipantPageViewModel(MainContext);
        }

        private void InitializeCommands()
        {
            GoToHomePageCommand = new RelayCommand(GoToHomePageCommandExecute,GoToHomePageCommandCanExecute);
            GoToSettingsPageCommand = new RelayCommand(GoToSettingsPageCommandExecute,GoToSettingsPageCommandCanExecute);
            GoToPatricipantsPageCommand = new RelayCommand(GoToPatricipantsPageCommandExecute, GoToPatricipantsPageCommandCanExecute);
            GoToJudgePageCommand = new RelayCommand(GoToJudgePageCommandExecute, GoToJudgePageCommandCanExecute);
            GoToCompetitionCommandPageCommand = new RelayCommand(GoToCompetitionCommandPageCommandExecute, GoToCompetitionCommandPageCommandCanExecute);
            GoToCompetitionFigurePageCommand = new RelayCommand(GoToCompetitionFigurePageCommandExecute,GoToCompetitionFigurePageCommandCanExecute);
            GoToGroupsPageCommand = new RelayCommand(GoToGroupsPageCommandExecute, GoToGroupsPageCommandCanExecute);
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
                CompetitionNavigator.Navigate(CompetitionPagesResolver.PatricipantsAlias, PatricipantsPageViewModel);
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
                CompetitionNavigator.Navigate(CompetitionPagesResolver.JudgeAlias, JudgePageViewModel);
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
                CompetitionNavigator.Navigate(CompetitionPagesResolver.CompetitionCommandAlias, CompetitionCommandPageViewModel);
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
                CompetitionNavigator.Navigate(CompetitionPagesResolver.CompetitionFigureAlias, CompetitionFigurePageViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GoToGroupsPageCommandExecute(object viewModel)
        {
            try
            {
                CurrentCompetitionURI = CompetitionPagesResolver.GroupsAlias;
                CompetitionNavigator.Navigate(CompetitionPagesResolver.GroupsAlias, GroupsPageViewModel);
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

        private bool GoToGroupsPageCommandCanExecute(object viewModel)
        {
            return CompetitionPagesResolver.GroupsAlias != CurrentCompetitionURI;
        }

    }
}
