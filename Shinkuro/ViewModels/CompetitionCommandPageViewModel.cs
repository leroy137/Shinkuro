using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;


namespace Shinkuro.ViewModels
{
    class CompetitionCommandPageViewModel : ViewModelBase
    {

        public ApplicationCoreContext Context { get; set; }

        public CompetitionCommandPageViewModel()
        {

        }

        public CompetitionCommandPageViewModel(ApplicationCoreContext context) : this()
        {
            Context = context;
        }

    }
}
