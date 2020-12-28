using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;

namespace Shinkuro.ViewModels
{
    class CompetitionFigurePageViewModel : ViewModelBase
    {

        public ApplicationCoreContext Context { get; set; }

        public CompetitionFigurePageViewModel()
        {

        }

        public CompetitionFigurePageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
        }

    }
}
