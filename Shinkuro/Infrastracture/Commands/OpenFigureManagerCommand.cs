using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.Infrastracture.Commands.Base;
using System.Windows;
using Shinkuro.Views.Windows;
using Shinkuro.Models;

namespace Shinkuro.Infrastracture.Commands
{
    class OpenFigureManagerCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                FigureManagerWindow figureManager = new FigureManagerWindow();
                figureManager.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!!!");
            }
        }
    }
}
