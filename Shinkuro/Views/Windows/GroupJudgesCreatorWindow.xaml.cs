using Shinkuro.Infrastracture.Commands;
using Shinkuro.Models;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shinkuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для GroupJudgesCreatorWindow.xaml
    /// </summary>
    public partial class GroupJudgesCreatorWindow : Window
    {
        public String GroupJudgesName { get; set; }

        public GroupJudges GroupJudgesNew { get; set; }

        public ObservableCollection<JudgeGroup> SelectedJudgesList { get; set; } = new ObservableCollection<JudgeGroup>();

        public ICommand CreateGroupJudgesCommand { get; set; }
        public ICommand AttachJudgeCommand { get; set; }
        public ICommand UnsetJudgeCommand { get; set; }

        public Judge SelectedJudge { get; set; }
        public JudgeGroup SelectedJudgeUnset { get; set; }
        public ICollectionView Judges { get; set; }
        public ICollectionView SelectedJudges { get; set; }
        public ICollectionView SelectedJudgesView { get; set; }

        public ApplicationCoreContext Context { get; set; }

        public GroupJudgesCreatorWindow()
        {
            InitializeComponent();
            window.DataContext = this;
            CreateGroupJudgesCommand = new RelayCommand(CreateGroupJudgesCommandExecute, CreateGroupJudgesCommandCanExecute);
            AttachJudgeCommand = new RelayCommand(AttachJudgeCommandExecute, AttachJudgeCommandCanExecute);
            UnsetJudgeCommand = new RelayCommand(UnsetJudgeCommandExecute, UnsetJudgeCommandCanExecute);
            SelectedJudgesView = CollectionViewSource.GetDefaultView(SelectedJudgesList);
        }

        public GroupJudgesCreatorWindow(ApplicationCoreContext context) : this()
        {
            Context = context;
            Judges = CollectionViewSource.GetDefaultView(Context.Judges);
            Judges.Refresh();
        }

        private void CreateGroupJudgesCommandExecute(object obj)
        {
            try
            {

                List<Judge> judges = new List<Judge>();
                for (int i = 0; i < SelectedJudgesList.Count; i++)
                    judges.Add(SelectedJudgesList[i].Judge);
                GroupJudges newGroup = new GroupJudges(GroupJudgesName, judges);
                GroupJudgesNew = newGroup;
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private bool CreateGroupJudgesCommandCanExecute(object obj)
        {
            return true;
        }

        private void AttachJudgeCommandExecute(object obj)
        {
            try
            {
                if (SelectedJudgesList.Select(c=>c.Judge).Contains(SelectedJudge))
                    throw new Exception("Ошибка, судья уже находится в списке выбранных судей!");

                SelectedJudgesList.Add(new JudgeGroup(SelectedJudgesList.Count+1, SelectedJudge));
                SelectedJudgesView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private bool AttachJudgeCommandCanExecute(object obj)
        {
            return SelectedJudge!= null;
        }

        private void UnsetJudgeCommandExecute(object obj)
        {
            try
            {
                JudgeGroup current = obj as JudgeGroup;
                if (current != null)
                {
                    SelectedJudgesList.Remove(current);
                    UpdateNumbersListJudgesGroup();
                    SelectedJudgesView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private bool UnsetJudgeCommandCanExecute(object obj)
        {
            return true;
        }

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void UpdateNumbersListJudgesGroup()
        {
            for(int i=0;i<SelectedJudgesList.Count;i++)
            {
                SelectedJudgesList[i].Number = i + 1;
            }
        }
    }
}
