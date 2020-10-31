using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shinkuro.Models
{
    internal class ApplicationCoreContext
    {
        public Competition CurrentCompetition { get; set; }
        public ObservableCollection<Patricipant> Patricipants { get; set; }
        public ObservableCollection<Judge> Judges { get; set; }
        public ApplicationCoreContext()
        {
            CurrentCompetition = null;
            Patricipants = new ObservableCollection<Patricipant>();
            Judges = new ObservableCollection<Judge>();
        }
    }
}
