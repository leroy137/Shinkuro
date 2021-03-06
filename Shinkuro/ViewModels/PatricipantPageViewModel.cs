﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.ViewModels.Base;
using System.Windows;
using Shinkuro.Models;
using System.Collections.ObjectModel;
using Shinkuro.Views.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace Shinkuro.ViewModels
{
    class PatricipantPageViewModel : ViewModelBase
    {
        private String _searchTextPatricipant;
        private String _cityPatricipatnFilter;
        private String _yearPatricipantFilter;
        private bool _completePatricipants;

        public CollectionViewSource CollectionView { get; set; } = new CollectionViewSource();

        public ApplicationCoreContext Context { get; set; }

        public String FIOPatricipantFilter 
        { 
            get { return _searchTextPatricipant; }
            set 
            { 
                Set<String>(ref _searchTextPatricipant, value);
                Patricipants.Refresh();
            }
        }

        public String CityPatricipantFilter 
        {
            get { return _cityPatricipatnFilter; }
            set 
            { 
                Set<String>(ref _cityPatricipatnFilter, value);
                Patricipants.Refresh();
            }
        }

        public String YearPatricipantFilter 
        { 
            get { return _yearPatricipantFilter; }
            set 
            { 
                Set<String>(ref _yearPatricipantFilter, value);
                Patricipants.Refresh();
            }
        }

        public bool CompletePatricipant 
        { 
            get { return _completePatricipants; }
            set 
            { 
                Set<Boolean>(ref _completePatricipants, value);
                Patricipants.Refresh();
            }
        }

        public Patricipant SelectedPatricipant { get; set; }
        public ICommand ResetFilterCommand { get; set; }
        public ICommand UpdateListPatricipantCommand { get; set; }
        public ICommand CreatePatricipantCommand { get; set; }
        public ICommand DeletePatricipantCommand { get; set; }
        public ICommand EditPatricipantCommand { get; set; }
        public ICommand ViewPatricipantCommand { get; set; }
        public ICommand ClearMessagesBlockCommand { get; set; }


        public ICollectionView Patricipants { get; set; }

        public ObservableCollection<MessageLog> MessageLogs { get; set; } = new ObservableCollection<MessageLog>();

        public PatricipantPageViewModel()
        {
            ResetFilterCommand = new RelayCommand(ResetFilterCommandExecute, ResetFilterCommandCanExecute);
            UpdateListPatricipantCommand = new RelayCommand(UpdateListPatricipantCommandExecute, UpdateListPatricipantrCommandCanExecute);
            DeletePatricipantCommand = new RelayCommand(DeletePatricipantCommandExecute, DeletePatricipantrCommandCanExecute);
            EditPatricipantCommand = new RelayCommand(EditPatricipantCommandExecute, EditPatricipantrCommandCanExecute);
            ViewPatricipantCommand = new RelayCommand(ViewPatricipantCommandExecute, ViewPatricipantrCommandCanExecute);
            CreatePatricipantCommand = new RelayCommand(CreatePatricipantCommandExecute, CreatePatricipantrCommandCanExecute);
            ClearMessagesBlockCommand = new RelayCommand(ClearMessagesBlockCommandExecute, ClearMessagesBlockCommandCanExecute);
        }

        public PatricipantPageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
            CollectionView.Source = context.Patricipants;
            Patricipants = CollectionView.View;
            Patricipants.Filter = FilterPatricipant;
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
                MessageBox.Show(ex.Message, "Ошибка!");
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
                Patricipants.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
                if (SelectedPatricipant == null)
                    throw new Exception("Участник для удаления не выбран!");

                var result = MessageBox.Show($"Удалить участника {SelectedPatricipant.Surname} {SelectedPatricipant.Name} (город {SelectedPatricipant.City})?", "Удаление участника", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {
                    String city = SelectedPatricipant.City;
                    String surname = SelectedPatricipant.Surname;
                    String name = SelectedPatricipant.Name;
                    Context.RemovePatricipant(SelectedPatricipant);
                    MessageLogs.Add(new MessageLog(LogType.Warrning, $"Участник {surname} {name} (город {city}) успешно удален!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                    String changes = edit.GetChanges(SelectedPatricipant);
                    Context.UpdatePatricipant(SelectedPatricipant, edit);
                    Patricipants.Refresh();
                    MessageLogs.Add(new MessageLog(LogType.Information, $"Изменение участника {SelectedPatricipant.ShortFIO}: {changes}!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                    Context.AddPatricipant(patricipantNew);
                    MessageLogs.Add(new MessageLog(LogType.Successfull, $"Участник {patricipantNew.ShortFIO} (город: {patricipantNew.City}) успешно добавлен!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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

        private bool FilterPatricipant(object obj)
        {
            bool result = true;
            Patricipant current = obj as Patricipant;
            if (current != null)
            {
                if (!String.IsNullOrWhiteSpace(FIOPatricipantFilter))
                    result = result && current.FIO.Contains(FIOPatricipantFilter);

                if (!String.IsNullOrWhiteSpace(CityPatricipantFilter))
                    result = result && current.City.Contains(CityPatricipantFilter);

                if (!String.IsNullOrWhiteSpace(YearPatricipantFilter))
                    result = result && current.Year.ToString().Contains(YearPatricipantFilter);

                if (CompletePatricipant)
                    result = result && (String.IsNullOrWhiteSpace(current.Surname) || String.IsNullOrWhiteSpace(current.Name) || String.IsNullOrWhiteSpace(current.City) || String.IsNullOrWhiteSpace(current.Year.ToString()));

                return result;
            }
            else
            {
                return false;
            }
        }

        private bool ClearMessagesBlockCommandCanExecute(Object obj)
        {
            return MessageLogs.Count != 0;
        }

        private void ClearMessagesBlockCommandExecute(Object obj)
        {
            try
            {
                MessageLogs.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
