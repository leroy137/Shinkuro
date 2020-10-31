using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using Shinkuro.Views.Windows;
using Shinkuro.Infrastracture.Commands;

namespace Shinkuro.ViewModels
{
    class JudgePageViewModel : ViewModelBase
    {
        private String _searchTextJudge;
        private String _cityJudgeFilter;
        private bool _completeJudges;

        private Judge _selectedJudge;

        public ApplicationCoreContext Context { get; set; }

        public String FIOJudgeFilter
        {
            get { return _searchTextJudge; }
            set { Set<String>(ref _searchTextJudge, value); }
        }

        public String CityJudgeFilter
        {
            get { return _cityJudgeFilter; }
            set { Set<String>(ref _cityJudgeFilter, value); }
        }

        public bool CompleteJudge
        {
            get { return _completeJudges; }
            set { Set<Boolean>(ref _completeJudges, value); }
        }

        public Judge SelectedJudge 
        { 
            get { return _selectedJudge; }
            set { Set<Judge>(ref _selectedJudge, value); }
        }

        public ICommand ResetFilterCommand { get; set; }
        public ICommand UpdateListJudgesCommand { get; set; }
        public ICommand CreateJudgeCommand { get; set; }
        public ICommand DeleteJudgeCommand { get; set; }
        public ICommand EditJudgeCommand { get; set; }
        public ICommand ViewJudgeCommand { get; set; }

        public ObservableCollection<Judge> Judges { get; set; }

        public JudgePageViewModel()
        {
            ResetFilterCommand = new RelayCommand(ResetFilterCommandExecute, ResetFilterCommandCanExecute);
            UpdateListJudgesCommand = new RelayCommand(UpdateListJudgesCommandExecute, UpdateListJudgesCommandCanExecute);
            DeleteJudgeCommand = new RelayCommand(DeleteJudgeCommandExecute, DeleteJudgeCommandCanExecute);
            EditJudgeCommand = new RelayCommand(EditJudgeCommandExecute, EditJudgeCommandCanExecute);
            ViewJudgeCommand = new RelayCommand(ViewJudgeCommandExecute, ViewJudgeCommandCanExecute);
            CreateJudgeCommand = new RelayCommand(CreateJudgeCommandExecute, CreateJudgeCommandCanExecute);
        }

        public JudgePageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
            Judges = context.Judges;
        }

        private void ResetFilterCommandExecute(object obj)
        {
            try
            {
                FIOJudgeFilter = "";
                CityJudgeFilter = "";
                CompleteJudge = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ResetFilterCommandCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(FIOJudgeFilter) || !String.IsNullOrEmpty(CityJudgeFilter)  || CompleteJudge != false;
        }


        private void UpdateListJudgesCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UpdateListJudgesCommandCanExecute(object obj)
        {
            return Context.Judges.Count != Judges.Count;
        }

        private void DeleteJudgeCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool DeleteJudgeCommandCanExecute(object obj)
        {
            return SelectedJudge != null;
        }

        private void EditJudgeCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool EditJudgeCommandCanExecute(object obj)
        {
            return SelectedJudge != null;
        }

        private void CreateJudgeCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool CreateJudgeCommandCanExecute(object obj)
        {
            return true;
        }

        private void ViewJudgeCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ViewJudgeCommandCanExecute(object obj)
        {
            return SelectedJudge != null;
        }
    }
}
