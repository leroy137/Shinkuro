using Shinkuro.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.ViewModels
{
    internal class MainWindowViewModel :ViewModelBase
    {
        private String _title;

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
