using System;
using System.Windows.Navigation;
using System.Windows.Controls;
using Shinkuro.Services.Navigation.Interfaces;

namespace Shinkuro.Services.Navigation
{
    public class CompetitionNavigator
    {
        private NavigationService _navService;
        private readonly IPageResolver _resolver;

        #region Properties

        public static NavigationService Service
        {
            get { return Instance._navService; }
            set
            {
                if (Instance._navService != null)
                {
                    Instance._navService.Navigated -= Instance._navService_Navigated;
                }

                Instance._navService = value;
                Instance._navService.Navigated += Instance._navService_Navigated;
            }
        }

        #endregion


        #region Public Methods

        public static void Navigate(Page page, object context)
        {
            if (Instance._navService == null || page == null)
            {
                return;
            }

            Instance._navService.Navigate(page, context);
        }

        public static void Navigate(Page page)
        {
            Navigate(page, null);
        }

        public static void Navigate(string uri, object context)
        {
            if (Instance._navService == null || uri == null)
            {
                return;
            }

            var page = Instance._resolver.GetPageInstance(uri);
            Navigate(page, context);
        }

        public static void Navigate(string uri)
        {
            Navigate(uri, null);
        }

        #endregion


        #region Private Methods

        void _navService_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;

            if (page == null)
            {
                return;
            }

            page.DataContext = e.ExtraData;
        }

        #endregion

        #region Singleton

        private static volatile CompetitionNavigator _instance;
        private static readonly object SyncRoot = new Object();

        private CompetitionNavigator()
        {
            _resolver = new CompetitionPagesResolver();
        }

        private static CompetitionNavigator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new CompetitionNavigator();
                    }
                }

                return _instance;
            }
        }
        #endregion
    }
}
