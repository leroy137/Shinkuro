using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;

namespace Shinkuro.ViewModels
{
    public class FigureSelectedViewModel : ViewModelBase
    {
        private string _filterText;
        public String FilterText
        {
            get { return _filterText; }
            set { Set<String>(ref _filterText, value); }
        }

        public ICommand SelectFiguresCommand { get; set; }
        public List<SelectedFigure> SelectedFigures { get; set; } = new List<SelectedFigure>();

        public FigureSelectedViewModel()
        {
            SelectFiguresCommand = new RelayCommand(SelectFiguresCommandExecute, SelectFiguresCommandCanExecute);
        }

        public FigureSelectedViewModel(List<Figure> selectedFigures) : this()
        {
        
            foreach(var figure in ApplicationCoreContext.Figures)
            {
                if(selectedFigures.Contains(figure))
                {
                    SelectedFigures.Add(new SelectedFigure(true, figure));
                }
                else
                {
                    SelectedFigures.Add(new SelectedFigure(false, figure));
                }
            }
        }

        private void SelectFiguresCommandExecute(Object obj)
        {

        }

        private bool SelectFiguresCommandCanExecute(Object obj)
        {
            return true;
        }
    }
}
