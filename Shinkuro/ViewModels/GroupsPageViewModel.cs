using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using System.Windows;
using Shinkuro.Views.Windows;
using System.Linq;

namespace Shinkuro.ViewModels
{
    class GroupsPageViewModel : ViewModelBase 
    {

        private Group _seletedGroup = null;
        private Figure _selectedGroupFigure = null;
        public ApplicationCoreContext Context { get; set; }
        public ICollectionView Groups { get; set; }

        public Group SelectedGroup 
        { 
            get { return _seletedGroup; } 
            set 
            { 
                Set<Group>(ref _seletedGroup, value);
            } 
        }

        public Figure SelectedGroupFigure
        {
            get { return _selectedGroupFigure; }
            set
            {
                Set<Figure>(ref _selectedGroupFigure, value);
            }
        }

        #region Команды

        public ICommand CreateGroupCommand { get; set; }
        public ICommand DeleteGroupCommand { get; set; }
        public ICommand EditGroupCommand { get; set; }
        public ICommand ViewGroupCommand { get; set; }

        public ICommand SelectFiguresCommand { get; set; }
        public ICommand UnsetFigureCommand { get; set; }

        public ICommand SelectPatricipantsCommand { get; set; }

        public ICommand UnsetPatricipantCommand { get; set; }

        #endregion

        public GroupsPageViewModel()
        {
            CreateGroupCommand = new RelayCommand(CreateGroupCommandExecute, CreateGroupCommandCanExecute);
            DeleteGroupCommand = new RelayCommand(DeleteGrouptCommandExecute, DeleteGroupCommandCanExecute);
            EditGroupCommand = new RelayCommand(EditGroupCommandExecute, EditGroupCommandCanExecute);
            ViewGroupCommand = new RelayCommand(ViewGroupCommandExecute, ViewGrouprCommandCanExecute);
            SelectFiguresCommand = new RelayCommand(SelectFiguresCommandExecute, SelectFiguresCommandCanExecute);
            UnsetFigureCommand = new RelayCommand(UnsetFigureCommandExecute, UnsetFigureCommandCanExecute);
            SelectPatricipantsCommand = new RelayCommand(SelectPatricipantsCommandExecute, SelectPatricipantsCommandCanExecute);
            UnsetPatricipantCommand = new RelayCommand(UnsetPatricipantCommandExecute, UnsetPatricipantCommandCanExecute);
        }

        public GroupsPageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
            Groups = CollectionViewSource.GetDefaultView(context.Groups);
        }

        private void DeleteGrouptCommandExecute(object obj)
        {
            try
            {
                if (SelectedGroup == null)
                    throw new Exception("Группа для удаления не выбрана!");

                var result = MessageBox.Show($"Удалить группу {SelectedGroup.Name}?", "Удаление группы", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {
                    String name = SelectedGroup.Name;
                    Context.RemoveGroup(SelectedGroup);
                    MessageBox.Show($"Группа {name} удалена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool DeleteGroupCommandCanExecute(object obj)
        {
            return SelectedGroup != null;
        }

        private void EditGroupCommandExecute(object obj)
        {
            try
            {

                if (SelectedGroup == null)
                    throw new Exception("Группа для изменения не выбрана!");


                GroupEditorWindow editorWindow = new GroupEditorWindow(SelectedGroup);
                editorWindow.ShowDialog();
                if (editorWindow.DialogResult == true)
                {
                    Group edit = editorWindow.GroupEdit;
                    Context.UpdateGroup(SelectedGroup, edit);
                    MessageBox.Show("Грппа успешно изменена!", "Изменение группы");
                    Groups.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool EditGroupCommandCanExecute(object obj)
        {
            return SelectedGroup != null;
        }

        private void CreateGroupCommandExecute(object obj)
        {
            try
            {
                GroupCreatorWindow groupCreatorWindow = new GroupCreatorWindow();
                groupCreatorWindow.ShowDialog();
                if(groupCreatorWindow.DialogResult == true)
                {
                    Group groupNew = groupCreatorWindow.GroupNew;
                    if(groupNew!=null)
                    {
                        Context.AddGroup(groupNew);
                        MessageBox.Show("Группа успешно создана!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool CreateGroupCommandCanExecute(object obj)
        {
            return true;
        }

        private void ViewGroupCommandExecute(object obj)
        {
            try
            {
                if (SelectedGroup == null)
                    throw new Exception("Группа для просмотра не выбрана!");

                GroupViewerWindow groupViewerWindow = new GroupViewerWindow(SelectedGroup);
                groupViewerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ViewGrouprCommandCanExecute(object obj)
        {
            return SelectedGroup != null;
        }

        private void SelectFiguresCommandExecute(Object obj)
        {
            try
            {
                if (SelectedGroup == null)
                    throw new Exception("Группа для заполенения списка фигур не выбрана!");

                FigureSelectedViewModel figureSelectedViewModel = new FigureSelectedViewModel(SelectedGroup.Figures);
                FiguresSeletedWindow figuresSeletedWindow = new FiguresSeletedWindow(figureSelectedViewModel);
                figuresSeletedWindow.ShowDialog();
                if(figuresSeletedWindow.DialogResult == true)
                {
                    List<Figure> selectedFigures = figureSelectedViewModel.SelectedFiguresGroup.ToList();
                    Context.SelectFiguresGroup(SelectedGroup, selectedFigures);
                    Group gr = SelectedGroup;
                    SelectedGroup = null;
                    SelectedGroup = gr;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool SelectFiguresCommandCanExecute(Object obj)
        {
            return SelectedGroup != null;
        }

        private void UnsetFigureCommandExecute(Object obj)
        {
            try
            {
                if (SelectedGroupFigure == null)
                    throw new Exception("Фигура для открепления от группы не задана!");

                if (!Context.UnsetGroupFigure(SelectedGroup, SelectedGroupFigure))
                    throw new Exception("Открепление фигуры не удалось!");

                Group gr = SelectedGroup;
                SelectedGroup = null;
                SelectedGroup = gr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UnsetFigureCommandCanExecute(Object obj)
        {
            return SelectedGroupFigure != null;
        }


        private void SelectPatricipantsCommandExecute(Object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool SelectPatricipantsCommandCanExecute(Object obj)
        {
            return SelectedGroup != null;
        }

        private void UnsetPatricipantCommandExecute(Object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UnsetPatricipantCommandCanExecute(Object obj)
        {
            return SelectedGroup != null;
        }
    }
}
