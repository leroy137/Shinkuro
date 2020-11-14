using System;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using System.Windows;
using Shinkuro.Views.Windows;
using System.Collections.ObjectModel;

namespace Shinkuro.ViewModels
{
    class FigureManagerViewModel : ViewModelBase
    {

        private string _filterText;

        public String FilterText
        {
            get { return _filterText; }
            set { Set<String>(ref _filterText, value); Figures.Refresh(); }
        }
        public ICollectionView Figures { get; set; }
        public Figure SelectedFigure { get; set; }
        public ObservableCollection<MessageLog> MessageLogs { get; set; } = new ObservableCollection<MessageLog>();

        public ICommand CreateFigureCommand { get; set; }
        public ICommand ViewFigureCommand { get; set; }
        public ICommand RemoveFigureCommand { get; set; }
        public ICommand EditFigureCommand { get; set; }
        public ICommand ClearMessagesBlockCommand { get; set; }
        public ICommand UpdateListFiguresCommand { get; set; }
        public CollectionViewSource CollectionView { get; set; } = new CollectionViewSource();
        public FigureManagerViewModel()
        {
            CollectionView.Source = ApplicationCoreContext.Figures;
            Figures = CollectionView.View;
            Figures.Filter = FilterFigure;

            UpdateListFiguresCommand = new RelayCommand(UpdateListFiguresCommandExecute, UpdateListFiguresCommandCanExecute);
            // инициализируем команды менеджера фигур
            CreateFigureCommand = new RelayCommand(CreateFigureCommandExecute, CreateFigureCommandCanExecute);
            ViewFigureCommand = new RelayCommand(ViewFigureCommandExecute, ViewFigureCommandCanExecute);
            RemoveFigureCommand = new RelayCommand(RemoveFigureCommandExecute, RemoveFigureCommandCanExecute);
            EditFigureCommand = new RelayCommand(EditFigureCommandExecute, EditFigureCommandCanExecute);
            ClearMessagesBlockCommand = new RelayCommand(ClearMessagesBlockCommandExecute, ClearMessagesBlockCommandCanExecute);
        }

        public bool FilterFigure(Object obj)
        {
            bool result = true;
            Figure current = obj as Figure;
            if (current != null)
            {
                if (!String.IsNullOrWhiteSpace(FilterText))
                    result = result && current.Name.Contains(FilterText);

                return result;
            }
            else
            {
                return false;
            }
        }

        private void CreateFigureCommandExecute(object obj)
        {
            try
            {
                FigureCreatorWindow figureCreatorWindow = new FigureCreatorWindow();
                figureCreatorWindow.ShowDialog();
                if (figureCreatorWindow.DialogResult == true)
                {
                    Figure figureNew = figureCreatorWindow.FigureNew;
                    ApplicationCoreContext.AddFigure(figureNew);
                    MessageLogs.Add(new MessageLog(LogType.Successfull, $"Фигура {figureNew.Name} успешно добавлена!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool CreateFigureCommandCanExecute(object obj)
        {
            return true;
        }

        private void RemoveFigureCommandExecute(object obj)
        {
            try
            {
                if (SelectedFigure == null)
                    throw new NullReferenceException("Фигура для удаления не задана и равна null");

                var result = MessageBox.Show($"Удалить фигуру {SelectedFigure.Name}?", "Удаление фигуры", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {

                    String name = SelectedFigure.Name;
                    if (ApplicationCoreContext.RemoveFigure(SelectedFigure))
                        MessageLogs.Add(new MessageLog(LogType.Successfull, $"Фигура {name} удалена!"));
                    else
                        throw new Exception("Ошибка операции удаления фигуры!");
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool RemoveFigureCommandCanExecute(object obj)
        {
            return SelectedFigure != null;
        }

        private void ViewFigureCommandExecute(object obj)
        {
            try
            {
                if (SelectedFigure == null)
                    throw new NullReferenceException("Фигура для просмотра не задана и равна null");

                FigureViewerWindow figureViewerWindow = new FigureViewerWindow(SelectedFigure);
                figureViewerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ViewFigureCommandCanExecute(object obj)
        {
            return SelectedFigure != null;
        }

        private void EditFigureCommandExecute(object obj)
        {
            try
            {
                if (SelectedFigure == null)
                    throw new Exception("Фигура для изменения не выбрана и равна null!");

                FigureEditorWindow editorWindow = new FigureEditorWindow(SelectedFigure);
                editorWindow.ShowDialog();
                if (editorWindow.DialogResult == true)
                {
                    Figure edit = editorWindow.FigureEdit;
                    ApplicationCoreContext.UpdateFigure(SelectedFigure, edit);
                    Figures.Refresh();
                    MessageLogs.Add(new MessageLog(LogType.Information, $"Фигура {edit.Name} успешно изменена!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool EditFigureCommandCanExecute(object obj)
        {
            return SelectedFigure != null;
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

        private void UpdateListFiguresCommandExecute(object obj)
        {
            try
            {
                Figures.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UpdateListFiguresCommandCanExecute(object obj)
        {
            return true;
        }
    }
}
