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

        public ObservableCollection<Patricipant> Patricipants { get; set; }

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
            Patricipants = context.Patricipants;
        }

        private void ResetFilterCommandExecute(object obj)
        {
            try
            {
                FIOPatricipantFilter = "";
                CityPatricipantFilter = "";
                YearPatricipantFilter = "";
                CompletePatricipant = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика!");
            }
        }

        private bool ResetFilterCommandCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(FIOPatricipantFilter) || !String.IsNullOrEmpty(CityPatricipantFilter) || !String.IsNullOrEmpty(YearPatricipantFilter) || CompletePatricipant != false;
        }


        private void UpdateListPatricipantCommandExecute(object obj)
        {
            try
            {
                Patricipants = Context.Patricipants;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UpdateListPatricipantrCommandCanExecute(object obj)
        {
            return Patricipants.Count != Context.Patricipants.Count;
        }

        private void DeletePatricipantCommandExecute(object obj)
        {
            try
            {
                if (SelectedPatricipant == null)
                    throw new Exception("Участник для удаления не выбран!");

                var result = MessageBox.Show($"Удалить участника {SelectedPatricipant.Firstname} {SelectedPatricipant.Surname} (город {SelectedPatricipant.City})?", "Удаление участника", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {
                    String city = SelectedPatricipant.City;
                    String firstname = SelectedPatricipant.Firstname;
                    String surname = SelectedPatricipant.Surname;
                    Context.Patricipants.Remove(SelectedPatricipant);
                    MessageBox.Show($"Участник {firstname} {surname} (город {city}) удален!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
                if (SelectedPatricipant == null)
                    throw new Exception("Участник для изменения не выбран!");

                PatricipantEditorWindow patricipantEditorWindow = new PatricipantEditorWindow(SelectedPatricipant);
                patricipantEditorWindow.ShowDialog();
                if(patricipantEditorWindow.DialogResult==true)
                {
                    Patricipant edit = patricipantEditorWindow.PatricipantEdit;
                    ApplicationCoreContext.UpdatePatricipant(SelectedPatricipant, edit);
                    MessageBox.Show("Участник успешно изменен!", "Изменение участника");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
                    Patricipant patricipantNew = patricipantCreatorWindow.PatricipantNew;
                    Context.Patricipants.Add(patricipantNew);
                    MessageBox.Show("Участник успешно добавлен!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
                if (SelectedPatricipant == null)
                    throw new Exception("Участник для просмотра не выбран!"); 

                PatricipantViewerWindow patricipantViewerWindow = new PatricipantViewerWindow(SelectedPatricipant);
                patricipantViewerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ViewPatricipantrCommandCanExecute(object obj)
        {
            return SelectedPatricipant!=null;
        }
    }
}
