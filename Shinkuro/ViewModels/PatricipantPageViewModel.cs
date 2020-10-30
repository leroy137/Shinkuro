using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.ViewModels.Base;
using System.Windows;
using Shinkuro.Models;
using System.Collections.ObjectModel;
using Shinkuro.Views.Windows;

namespace Shinkuro.ViewModels
{
    class PatricipantPageViewModel : ViewModelBase
    {
        private String _searchTextPatricipant;
        private String _cityPatricipatnFilter;
        private String _yearPatricipantFilter;
        private bool _completePatricipants;

        private Patricipant _selectedPatricipant;

        public ApplicationCoreContext Context { get; set; }

        public String FIOPatricipantFilter 
        { 
            get { return _searchTextPatricipant; }
            set { Set<String>(ref _searchTextPatricipant, value); }
        }

        public String CityPatricipantFilter 
        {
            get { return _cityPatricipatnFilter; }
            set { Set<String>(ref _cityPatricipatnFilter, value); }
        }

        public String YearPatricipantFilter 
        { 
            get { return _yearPatricipantFilter; }
            set { Set<String>(ref _yearPatricipantFilter, value); }
        }

        public bool CompletePatricipant 
        { 
            get { return _completePatricipants; }
            set { Set<Boolean>(ref _completePatricipants, value); }
        }

        public Patricipant SelectedPatricipant { get; set; }

        public ICommand ResetFilterCommand { get; set; }
        public ICommand UpdateListPatricipantCommand { get; set; }
        public ICommand CreatePatricipantCommand { get; set; }
        public ICommand DeletePatricipantCommand { get; set; }
        public ICommand EditPatricipantCommand { get; set; }
        public ICommand ViewPatricipantCommand { get; set; }


        public PatricipantPageViewModel()
        {
            ResetFilterCommand = new RelayCommand(ResetFilterCommandExecute, ResetFilterCommandCanExecute);
            UpdateListPatricipantCommand = new RelayCommand(UpdateListPatricipantCommandExecute, UpdateListPatricipantrCommandCanExecute);
            DeletePatricipantCommand = new RelayCommand(DeletePatricipantCommandExecute, DeletePatricipantrCommandCanExecute);
            EditPatricipantCommand = new RelayCommand(EditPatricipantCommandExecute, EditPatricipantrCommandCanExecute);
            ViewPatricipantCommand = new RelayCommand(ViewPatricipantCommandExecute, ViewPatricipantrCommandCanExecute);
            CreatePatricipantCommand = new RelayCommand(CreatePatricipantCommandExecute, CreatePatricipantrCommandCanExecute);
        }

        public PatricipantPageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
        }

        private void ResetFilterCommandExecute(object obj)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool ResetFilterCommandCanExecute(object obj)
        {
            return true;
        }


        private void UpdateListPatricipantCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool UpdateListPatricipantrCommandCanExecute(object obj)
        {
            return true;
        }

        private void DeletePatricipantCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool DeletePatricipantrCommandCanExecute(object obj)
        {
            return SelectedPatricipant != null;
        }

        private void EditPatricipantCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool EditPatricipantrCommandCanExecute(object obj)
        {
            return SelectedPatricipant != null;
        }

        private void CreatePatricipantCommandExecute(object obj)
        {
            try
            {
                PatricipantCreatorWindow patricipantCreatorWindow = new PatricipantCreatorWindow();
                patricipantCreatorWindow.ShowDialog();
                if(patricipantCreatorWindow.DialogResult == true)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool CreatePatricipantrCommandCanExecute(object obj)
        {
            return true;
        }

        private void ViewPatricipantCommandExecute(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool ViewPatricipantrCommandCanExecute(object obj)
        {
            return SelectedPatricipant!=null;
        }
    }
}
