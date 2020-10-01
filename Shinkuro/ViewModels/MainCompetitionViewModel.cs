using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using System.Windows.Input;

namespace Shinkuro.ViewModels
{
    /// <summary>
    /// главная вью модель окна управления текущим соревнованием
    /// </summary>
    internal class MainCompetitionViewModel : ViewModelBase
    {

        #region Команды
        
        // закрытие приложения
        public ICommand CloseApplicationCommand { get; private set; } = new CloseApplicationCommand();

        #endregion

    }
}
