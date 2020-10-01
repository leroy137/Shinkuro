using Shinkuro.Infrastracture.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Shinkuro.Infrastracture.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                var result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // если да то закрываем приложение
                    Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!!!");
            }
        }
    }
}
