using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;

namespace Shinkuro.ViewModels
{
    internal class SettingsPageViewModel : ViewModelBase
    {
        public Competition Competition { get; set; }

        public SettingsPageViewModel(Competition competition)
        {
            Competition = competition;
        }
    }
}
