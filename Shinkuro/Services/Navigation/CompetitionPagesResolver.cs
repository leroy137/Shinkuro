using System;
using System.Collections.Generic;
using Shinkuro.Services.Navigation.Interfaces;
using Shinkuro.Views.MainAppPages;
using Shinkuro.ViewModels;
using System.Windows.Controls;
using System.ComponentModel;

namespace Shinkuro.Services.Navigation
{
    class CompetitionPagesResolver : IPageResolver
    {
        private readonly Dictionary<string, Page> _pagesResolvers = new Dictionary<string, Page>();
        private readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();

        public String HomeAlias => "Home";
        public String PatricipantsAlias => "Patricipants";
        public String JudgeAlias => "Judge";
        public String CompetitionCommandAlias => "CompetitionCommand";
        public String CompetitionFigureAlias => "CompetitionFigure";
        public String SettingsAlias => "Settings";


        public CompetitionPagesResolver()
        {
            // pages
            _pagesResolvers.Add(HomeAlias, new HomePage());
            _pagesResolvers.Add(PatricipantsAlias, new PatricipantPage());
            _pagesResolvers.Add(JudgeAlias, new JudgePage());
            _pagesResolvers.Add(CompetitionCommandAlias, new CompetitionCommandPage());
            _pagesResolvers.Add(CompetitionFigureAlias, new CompetitionFigurePage());
            _pagesResolvers.Add(SettingsAlias, new SettingsPage());

            // view models
            _vmResolvers.Add(HomeAlias, () => new HomePageViewModel());
            _vmResolvers.Add(SettingsAlias, () => new SettingsPageViewModel());
        }

        public Page GetPageInstance(string alias)
        {
            if (_pagesResolvers.ContainsKey(alias))
            {
                return _pagesResolvers[alias];
            }

            return _pagesResolvers[HomeAlias];
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
            if (_vmResolvers.ContainsKey(alias))
            {
                return _vmResolvers[alias]();
            }

            return _vmResolvers[HomeAlias]();
        }
    }
}
