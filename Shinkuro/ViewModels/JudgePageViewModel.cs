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

        public ICollectionView Judges { get; set; }

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
            Judges = CollectionViewSource.GetDefaultView(Context.Judges);
            Judges.Filter = FilterJudge;
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
                Judges.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
                    MessageBox.Show($"Судья {surname} {name} (город {city}) удален!");
                }
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
                if (SelectedJudge == null)
                    throw new Exception("Судья для изменения не выбран!");

                JudgeEditorWindow editorWindow = new JudgeEditorWindow(SelectedJudge);
                editorWindow.ShowDialog();
                if (editorWindow.DialogResult == true)
                {
                    Judge edit = editorWindow.JudgeEdit;
                    Context.UpdateJudge(SelectedJudge, edit);
                    MessageBox.Show("Судья успешно изменен!", "Изменение судьи");
                }
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
                JudgeCreatorWindow judgeCreatorWindow = new JudgeCreatorWindow();
                judgeCreatorWindow.ShowDialog();
                if (judgeCreatorWindow.DialogResult == true)
                {
                    Judge judgeNew = judgeCreatorWindow.JudgeNew;
                    Context.AddJudge(judgeNew);
                    MessageBox.Show("Судья успешно добавлен!");
                }
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
    }
}
