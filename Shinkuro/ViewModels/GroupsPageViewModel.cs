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
using System.Collections.ObjectModel;

namespace Shinkuro.ViewModels
{
    class GroupsPageViewModel : ViewModelBase 
    {

        private Group _seletedGroup = null;
        private FigureGroup _selectedGroupFigure = null;
        private PatricipantGroup _selectedGroupPatricipant = null;
        private GroupJudgesFigure _seletedGroupJudgesFigure = null;

        public ApplicationCoreContext Context { get; set; }
        public ICollectionView Groups { get; set; }
        public ObservableCollection<MessageLog> MessageLogs { get; set; } = new ObservableCollection<MessageLog>();
        public Group SelectedGroup 
        { 
            get { return _seletedGroup; } 
            set 
            { 
                Set<Group>(ref _seletedGroup, value);
            } 
        }

        public FigureGroup SelectedGroupFigure
        {
            get { return _selectedGroupFigure; }
            set
            {
                Set<FigureGroup>(ref _selectedGroupFigure, value);
            }
        }

        public PatricipantGroup SelectedGroupPatricipant
        {
            get { return _selectedGroupPatricipant; }
            set
            {
                Set<PatricipantGroup>(ref _selectedGroupPatricipant, value);
            }
        }

        public GroupJudgesFigure SelectedGroupJudgesFigure 
        { 
            get { return _seletedGroupJudgesFigure; } 
            set { Set<GroupJudgesFigure>(ref _seletedGroupJudgesFigure, value); } 
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
        public ICommand ClearMessagesBlockCommand { get; set; }
        public ICommand AutofillGroupsCommand { get; set; }
        public ICommand AppendFigureJudgesCommand { get; set; }
        public ICommand UnsetFigureJudgesCommand { get; set; }
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
            ClearMessagesBlockCommand = new RelayCommand(ClearMessagesBlockCommandExecute, ClearMessagesBlockCommandCanExecute);
            AutofillGroupsCommand = new RelayCommand(AutofillGroupsCommandExecute, AutofillGroupsCommandCanExecute);
            AppendFigureJudgesCommand = new RelayCommand(AppendFigureJudgesCommandExecute, AppendFigureJudgesCommandCanExecute);
            UnsetFigureJudgesCommand = new RelayCommand(UnsetFigureJudgesCommandExecute, UnsetFigureJudgesCommandCanExecute);
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
                    MessageLogs.Add(new MessageLog(LogType.Warrning,$"Группа {name} удалена!"));
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error,ex.Message));
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
                    MessageLogs.Add(new MessageLog(LogType.Information, "Группа успешно изменена!"));
                    Groups.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                        MessageLogs.Add(new MessageLog(LogType.Successfull, $"Группа {groupNew.Name} успешно создана!"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                    int countPrevCountFigures = SelectedGroup.Figures.Count;
                    List<Figure> selectedFigures = figureSelectedViewModel.SelectedFiguresGroup.ToList();
                    Context.SelectFiguresGroup(SelectedGroup, selectedFigures);
                    Group gr = SelectedGroup;
                    SelectedGroup = null;
                    SelectedGroup = gr;
                    MessageLogs.Add(new MessageLog(LogType.Information, $"Список фигур для группы {SelectedGroup.Name} обновлен (число фигур {countPrevCountFigures} -> {selectedFigures.Count})!"));
                }
            }
            catch(Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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


                String figureNameUnset = SelectedGroupFigure.Figure.Name;
                if (!Context.UnsetGroupFigure(SelectedGroup, SelectedGroupFigure))
                    throw new Exception("Открепление фигуры не удалось!");

                Group gr = SelectedGroup;
                SelectedGroup = null;
                SelectedGroup = gr;
                MessageLogs.Add(new MessageLog(LogType.Information, $"Фигура {figureNameUnset} успешно откреплена от группы {SelectedGroup.Name}!"));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
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
                if (SelectedGroupPatricipant == null)
                    throw new Exception("Участник для открепления от группы не задан!");

                String fio = SelectedGroupPatricipant.Patricipant.FIO;
                String logMessage = $"Участник {SelectedGroupPatricipant.Patricipant.FIO} (город {SelectedGroupPatricipant.Patricipant.City}) успешно откреплен от группы {SelectedGroup.Name}!";
                if (!Context.UnsetGroupPatricipant(SelectedGroup, SelectedGroupPatricipant))
                    throw new Exception($"Открепление участника {fio} не удалось!");

                Group gr = SelectedGroup;
                SelectedGroup = null;
                SelectedGroup = gr;
                MessageLogs.Add(new MessageLog(LogType.Information, logMessage));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool UnsetPatricipantCommandCanExecute(Object obj)
        {
            return SelectedGroupPatricipant != null;
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

        private bool AutofillGroupsCommandCanExecute(Object obj)
        {
            return true;
        }

        private void AutofillGroupsCommandExecute(Object obj)
        {
            try
            {
                Context.AutoFillGroups();
                Group g = SelectedGroup;
                SelectedGroup = null;
                SelectedGroup = g;
                MessageLogs.Add(new MessageLog(LogType.Successfull, "Участники успешно распределены по возрастным группам!"));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool AppendFigureJudgesCommandCanExecute(Object obj)
        {
            return SelectedGroup!=null;
        }

        private void AppendFigureJudgesCommandExecute(Object obj)
        {
            try
            {
                GroupJudgesFigureCreatorViewModel viewModel = new GroupJudgesFigureCreatorViewModel(Context);
                GroupJudgesFigureCreatorWindow groupJudgesFigureCreator = new GroupJudgesFigureCreatorWindow();
                groupJudgesFigureCreator.DataContext = viewModel;
                groupJudgesFigureCreator.ShowDialog();
                if(groupJudgesFigureCreator.DialogResult == true)
                {
                    GroupJudgesFigure groupJudgesFigure = viewModel.GroupJudgesFigureNew;
                    Context.AddGroupJudgesFigure(SelectedGroup, groupJudgesFigure);
                    Group g = SelectedGroup;
                    SelectedGroup = null;
                    SelectedGroup = g;
                }
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }

        private bool UnsetFigureJudgesCommandCanExecute(Object obj)
        {
            return SelectedGroupJudgesFigure!=null&&SelectedGroup!=null;
        }

        private void UnsetFigureJudgesCommandExecute(Object obj)
        {
            try
            {

                String logMessage = $"Фигура {SelectedGroupJudgesFigure.Figure.Name} успешно откреплена от группы {SelectedGroup.Name}";
                Context.UnsetGroupJudgesFigure(SelectedGroup, SelectedGroupJudgesFigure);
                Group gr = SelectedGroup;
                SelectedGroup = null;
                SelectedGroup = gr;
                MessageLogs.Add(new MessageLog(LogType.Information, logMessage));
            }
            catch (Exception ex)
            {
                MessageLogs.Add(new MessageLog(LogType.Error, ex.Message));
            }
        }
    }
}
