using Shinkuro.Infrastracture.Commands;
using Shinkuro.Models;
using Shinkuro.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shinkuro.ViewModels
{
    public class GroupJudgesFigureCreatorViewModel : ViewModelBase
    {

        private Figure _selectedFigure;
        private GroupJudges _selectedGroupJudges;
        public ApplicationCoreContext Context { get; set; }

        public Figure FigureSelected
        {
            get { return _selectedFigure; }
            set { Set<Figure>(ref _selectedFigure, value); }
        }

        public GroupJudges JudgesSelected 
        { 
            get { return _selectedGroupJudges; } 
            set { Set<GroupJudges>(ref _selectedGroupJudges, value); }
        }
        public GroupJudgesFigure GroupJudgesFigureNew { get; set; }

        public ICollectionView Figures
        {
            get;
            set;
        }

        public ICollectionView GroupJudgesList { get; set; }
        public ICollectionView ItemsListView { get; set; }
        public ICommand AppendGroupJudgesFigureCommand { get; set; }
        public GroupJudgesFigureCreatorViewModel()
        {

        }

        public GroupJudgesFigureCreatorViewModel(ApplicationCoreContext context)
        {
            CollectionViewSource collectionView = new CollectionViewSource();
            collectionView.Source = ApplicationCoreContext.Figures;
            AppendGroupJudgesFigureCommand = new RelayCommand(AppendGroupJudgesFigureExecute, AppendGroupJudgesFigureCanExecute);
            Figures = collectionView.View;//collectionView.GetDefaultView(ApplicationCoreContext.Figures);
            Context = context;
            GroupJudgesList = CollectionViewSource.GetDefaultView(Context.GroupJudges);
        }

        private bool AppendGroupJudgesFigureCanExecute(Object obj)
        {
            return FigureSelected != null && JudgesSelected != null;
        }

        private void AppendGroupJudgesFigureExecute(Object obj)
        {
            try
            {
                Window window = obj as Window;
                if(window!=null)
                {
                    if (FigureSelected == null)
                        throw new Exception("Фигура не выбрана, пожалуйста выберете фигуру!");

                    if (JudgesSelected == null)
                        throw new Exception("Бригада судей для оценивания фигуры не выбрана!");

                    GroupJudgesFigureNew = new GroupJudgesFigure(1, FigureSelected, JudgesSelected);
                    window.DialogResult = true;
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
