using Shinkuro.Infrastracture.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Shinkuro.Infrastracture.Commands
{
    internal class OpenCompetitionCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!!!");
            }
        }
    }
}
