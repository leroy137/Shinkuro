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

namespace Shinkuro.ViewModels
{
    class GroupsPageViewModel : ViewModelBase 
    {

        private Group _seletedGroup = null;
        public ApplicationCoreContext Context { get; set; }
        public ICollectionView Groups { get; set; }

        public Group SelectedGroup 
        { 
            get { return _seletedGroup; } 
            set { Set<Group>(ref _seletedGroup, value); } 
        }

        #region Команды

        public ICommand CreateGroupCommand { get; set; }
        public ICommand DeleteGroupCommand { get; set; }
        public ICommand EditGroupCommand { get; set; }
        public ICommand ViewGroupCommand { get; set; }

        #endregion

        public GroupsPageViewModel()
        {
            CreateGroupCommand = new RelayCommand(CreateGroupCommandExecute, CreateGroupCommandCanExecute);
            DeleteGroupCommand = new RelayCommand(DeleteGrouptCommandExecute, DeleteGroupCommandCanExecute);
            EditGroupCommand = new RelayCommand(EditGroupCommandExecute, EditGroupCommandCanExecute);
            ViewGroupCommand = new RelayCommand(ViewGroupCommandExecute, ViewGrouprCommandCanExecute);
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
    }
}
