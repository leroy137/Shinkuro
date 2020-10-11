using Shinkuro.ViewModels.Base;
using System;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using Shinkuro.Services.Navigation;
using System.Windows.Input;
using System.Windows;
namespace Shinkuro.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private String _title = "Shinkuro";

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public String Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
            }
        }
    }
}
