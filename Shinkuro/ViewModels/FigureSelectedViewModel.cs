using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace Shinkuro.ViewModels
{
    public class FigureSelectedViewModel : ViewModelBase
    {
        private string _filterText;
        public String FilterText
        {
            get { return _filterText; }
            set { Set<String>(ref _filterText, value); SelectedFigures.Refresh(); }
        }

        public ICommand SelectFiguresCommand { get; set; }
        public List<SelectedFigure> SelectedFiguresList { get; set; } = new List<SelectedFigure>();
        public IEnumerable<Figure> SelectedFiguresGroup => SelectedFiguresList.Where(sf=>sf.IsSelected).Select(sf => sf.Figure);
        public ICollectionView SelectedFigures { get; set; }

        public FigureSelectedViewModel()
        {
            SelectFiguresCommand = new RelayCommand(SelectFiguresCommandExecute, SelectFiguresCommandCanExecute);

        }

        public FigureSelectedViewModel(List<FigureGroup> selectedFigures) : this()
        {
            var selectedFiguresGroup = selectedFigures.Select(fg => fg.Figure);
            foreach (var figure in ApplicationCoreContext.Figures)
            {
                if(selectedFiguresGroup.Contains(figure))
                {
                    SelectedFiguresList.Add(new SelectedFigure(true, figure));
                }
                else
                {
                    SelectedFiguresList.Add(new SelectedFigure(false, figure));
                }
            }
            SelectedFigures = CollectionViewSource.GetDefaultView(SelectedFiguresList);
            SelectedFigures.Filter = FilterFigure;
            SelectedFigures.Refresh();
        }

        private void SelectFiguresCommandExecute(Object obj)
        {
            Window window = obj as Window;
            if(window!=null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        private bool SelectFiguresCommandCanExecute(Object obj)
        {
            return true;
        }

        private bool FilterFigure(object obj)
        {
            bool result = true;
            SelectedFigure current = obj as SelectedFigure;
            if (current != null)
            {
                if (!String.IsNullOrWhiteSpace(FilterText))
                    result = current.Figure.Name.Contains(FilterText);

                return result;
            }
            return false;
        }
    }
}
