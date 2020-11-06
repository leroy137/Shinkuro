using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using System.Windows.Input;
using System.Windows;

namespace Shinkuro.ViewModels
{
    internal class SettingsPageViewModel : ViewModelBase
    {

        private Competition _innerCompetition;
        private Competition _cloneCompetition;

        private string _competitionName;

        public Competition Competition 
        { 
            get { return _innerCompetition; } 
            set { Set<Competition>(ref _innerCompetition, value); } 
        }
        public Competition CompetitionCloneView
        {
            get { return _cloneCompetition; }
            set { Set<Competition>(ref _cloneCompetition, value); }
        }

        public String CompetitionName
        {
            get { return _competitionName; }
            set { Set<String>(ref _competitionName, value); }
        }

        public ICommand SaveSettingsCommand { get; set; }
        public ICommand ResetSettingsCommand { get; set; }
        public ICommand UploadLogoCompetition { get; set; }
        public ICommand CancelChangeLogo { get; set; }

        public SettingsPageViewModel(Competition competition)
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute, SaveSettingsCommandCanExecute);
            ResetSettingsCommand = new RelayCommand(ResetSettingsCommandExecute, ResetSettingsCommandCanExecute);
            UploadLogoCompetition = new RelayCommand(UploadLogoCompetitionExecute, UploadLogoCompetitionCanExecute);
            CancelChangeLogo = new RelayCommand(CancelChangeLogoExecute, CancelChangeLogoCanExecute);

            Competition = competition;
            CompetitionName = competition.Name;
            CompetitionCloneView = new Competition(Competition.Name, Competition.StartDate, Competition.FinishDate, Competition.Description, Competition.Place, Competition.Organizator, Competition.Contacts);
        }

        private void SaveSettingsCommandExecute(object obj)
        {
            try
            {
                CompetitionCloneView.Name = CompetitionName;
                Competition.UpdateCompetition(CompetitionCloneView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool SaveSettingsCommandCanExecute(object obj)
        {
            return !(Competition.Equals(CompetitionCloneView) && CompetitionName == Competition.Name);
        }

        private void ResetSettingsCommandExecute(object vobj)
        {
            try
            {
                Competition c = new Competition(Competition.Name, Competition.StartDate, Competition.FinishDate, Competition.Description, Competition.Place, Competition.Organizator, Competition.Contacts);
                CompetitionCloneView = c;
                CompetitionName = c.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool ResetSettingsCommandCanExecute(object obj)
        {
            return !(Competition.Equals(CompetitionCloneView)&&CompetitionName==Competition.Name);
        }

        private void UploadLogoCompetitionExecute(Object obj)
        {

        }

        private bool UploadLogoCompetitionCanExecute(Object obj)
        {
            return true;
        }

        private void CancelChangeLogoExecute(Object obj)
        {

        }

        private bool CancelChangeLogoCanExecute(Object obj)
        {
            return true;
        }

    }
}
