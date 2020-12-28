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
using System.ComponentModel;
using System.Windows.Data;

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
            set { Set<String>(ref _searchTextJudge, value); Judges.Refresh(); }
        }

        public String CityJudgeFilter
        {
            get { return _cityJudgeFilter; }
            set { Set<String>(ref _cityJudgeFilter, value); Judges.Refresh(); }
        }

        public bool CompleteJudge
        {
            get { return _completeJudges; }
            set { Set<Boolean>(ref _completeJudges, value); Judges.Refresh(); }
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
        public ICommand ClearMessagesBlockCommand { get; set; }
        public ICollectionView Judges { get; set; }
        public CollectionViewSource CollectionView { get; set; } = new CollectionViewSource();
        public ObservableCollection<MessageLog> MessageLogs { get; set; } = new ObservableCollection<MessageLog>();

        public JudgePageViewModel()
        {
            ResetFilterCommand = new RelayCommand(ResetFilterCommandExecute, ResetFilterCommandCanExecute);
            UpdateListJudgesCommand = new RelayCommand(UpdateListJudgesCommandExecute, UpdateListJudgesCommandCanExecute);
            DeleteJudgeCommand = new RelayCommand(DeleteJudgeCommandExecute, DeleteJudgeCommandCanExecute);
            EditJudgeCommand = new RelayCommand(EditJudgeCommandExecute, EditJudgeCommandCanExecute);
            ViewJudgeCommand = new RelayCommand(ViewJudgeCommandExecute, ViewJudgeCommandCanExecute);
            CreateJudgeCommand = new RelayCommand(CreateJudgeCommandExecute, CreateJudgeCommandCanExecute);
            ClearMessagesBlockCommand = new RelayCommand(ClearMessagesBlockCommandExecute, ClearMessagesBlockCommandCanExecute);
        }

        public JudgePageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
            CollectionView.Source = Context.Judges;
            Judges = CollectionView.View;
            Judges.Filter = FilterJudge;
        }

        private void ResetFilterCommandExecute(object obj)
        {
            try
            {
                FIOJudgeFilter = "";
                CityJudgeFilter = "";
                CompleteJudge = false;
                MessageLogs.Add(new MessageLog(LogType.Information, "Фильтры сброшены"));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                Judges.Refresh();
                MessageLogs.Add(new MessageLog(LogType.Information, "Список судей обновлен!"));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool UpdateListJudgesCommandCanExecute(object obj)
        {
            return true;
        }

        private void DeleteJudgeCommandExecute(object obj)
        {
            try
            {
                if (SelectedJudge == null)
                    throw new Exception("Судья для удаления не выбран!");

                var result = MessageBox.Show($"Удалить судью {SelectedJudge.Surname} {SelectedJudge.Name} (город {SelectedJudge.City})?", "Удаление судьи", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {
                    String city = SelectedJudge.City;
                    String surname = SelectedJudge.Surname;
                    String name = SelectedJudge.Name;
                    Context.RemoveJudge(SelectedJudge);
                    MessageLogs.Add(new MessageLog(LogType.Warrning, $"Судья { surname } { name} (город { city}) удален!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                if (SelectedJudge == null)
                    throw new Exception("Судья для изменения не выбран!");

                JudgeEditorWindow editorWindow = new JudgeEditorWindow(SelectedJudge);
                editorWindow.ShowDialog();
                if (editorWindow.DialogResult == true)
                {
                    Judge edit = editorWindow.JudgeEdit;
                    String changes = edit.GetChanges(SelectedJudge);
                    Context.UpdateJudge(SelectedJudge, edit);
                    Judges.Refresh();
                    MessageLogs.Add(new MessageLog(LogType.Information, $"Изменение судьи {SelectedJudge.ShortFIO}: {changes}!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                JudgeCreatorWindow judgeCreatorWindow = new JudgeCreatorWindow();
                judgeCreatorWindow.ShowDialog();
                if (judgeCreatorWindow.DialogResult == true)
                {
                    Judge judgeNew = judgeCreatorWindow.JudgeNew;
                    Context.AddJudge(judgeNew);
                    MessageLogs.Add(new MessageLog(LogType.Successfull, $"Судья {judgeNew.ShortFIO} успешно добавлен!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                if (SelectedJudge == null)
                    throw new Exception("Судья для просмотра не выбран!");

                JudgeViewerWindow judgeViewerWindow = new JudgeViewerWindow(SelectedJudge);
                judgeViewerWindow.ShowDialog();
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

        public bool FilterJudge(Object obj)
        {
            bool result = true;
            Judge current = obj as Judge;
            if (current != null)
            {
                if (!String.IsNullOrWhiteSpace(FIOJudgeFilter))
                    result = result && current.FIO.Contains(FIOJudgeFilter);

                if (!String.IsNullOrWhiteSpace(CityJudgeFilter))
                    result = result && current.City.Contains(CityJudgeFilter);

                if (CompleteJudge)
                    result = result && (String.IsNullOrWhiteSpace(current.Post) || String.IsNullOrWhiteSpace(current.Rank));

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
