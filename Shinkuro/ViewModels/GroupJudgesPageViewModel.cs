using Shinkuro.Infrastracture.Commands;
using Shinkuro.Models;
using Shinkuro.ViewModels.Base;
using Shinkuro.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shinkuro.ViewModels
{
    class GroupJudgesPageViewModel : ViewModelBase
    {
        public GroupJudges SelectedGroupJudges { get; set; }

        public ApplicationCoreContext Context { get; set; }

        public ObservableCollection<MessageLog> MessageLogs { get; set; } = new ObservableCollection<MessageLog>();
        public ICollectionView GroupsJudges { get; set; }

        public ICommand CreateGroupJudgesCommand { get; set; }
        public ICommand DeleteGroupJudgesCommand { get; set; }
        public ICommand ClearMessagesBlockCommand { get; set; }
        public ICommand UpdateListGroupJudgesCommand { get; set; }

        public GroupJudgesPageViewModel()
        {
            ClearMessagesBlockCommand = new RelayCommand(ClearMessagesBlockCommandExecute, ClearMessagesBlockCommandCanExecute);
            CreateGroupJudgesCommand = new RelayCommand(CreateGroupJudgesCommandExecute, CreateGroupJudgesCommandCanExecute);
            DeleteGroupJudgesCommand = new RelayCommand(DeleteGroupJudgesCommandExecute, DeleteGroupJudgesCommandCanExecute);
            UpdateListGroupJudgesCommand = new RelayCommand(UpdateListGroupJudgesCommandExecute, UpdateListGroupJudgesCommandCanExecute);
        }

        public GroupJudgesPageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
            GroupsJudges = CollectionViewSource.GetDefaultView(Context.GroupJudges);
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

        private void UpdateListGroupJudgesCommandExecute(object obj)
        {
            try
            {
                GroupsJudges.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UpdateListGroupJudgesCommandCanExecute(object obj)
        {
            return true;
        }

        private void DeleteGroupJudgesCommandExecute(object obj)
        {
            try
            {
                if (SelectedGroupJudges == null)
                    throw new Exception("Бригада судей для удаления не выбран!");

                var result = MessageBox.Show($"Удалить участника {SelectedGroupJudges.Name}?", "Удаление бригады", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то удаляем
                {
                    String name = SelectedGroupJudges.Name;
                    Context.RemoveGroupJudges(SelectedGroupJudges);
                    MessageLogs.Add(new MessageLog(LogType.Warrning, $"Бригада судей {name} успешно удалена!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool DeleteGroupJudgesCommandCanExecute(object obj)
        {
            return SelectedGroupJudges != null;
        }

        private void CreateGroupJudgesCommandExecute(object obj)
        {
            try
            {
                GroupJudgesCreatorWindow groupJudgesCreator = new GroupJudgesCreatorWindow(Context);
                groupJudgesCreator.ShowDialog();
                if (groupJudgesCreator.DialogResult == true)
                {
                    //Patricipant patricipantNew = patricipantCreatorWindow.PatricipantNew;
                    //Context.AddPatricipant(patricipantNew);
                    //MessageLogs.Add(new MessageLog(LogType.Successfull, $"Участник {patricipantNew.ShortFIO} (город: {patricipantNew.City}) успешно добавлен!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool CreateGroupJudgesCommandCanExecute(object obj)
        {
            return true;
        }
    }
}
