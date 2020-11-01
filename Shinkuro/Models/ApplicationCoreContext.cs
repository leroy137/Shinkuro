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

        public static void UpdatePatricipant(Patricipant destination, Patricipant source)
        {
            destination.Firstname = source.Firstname;
            destination.Surname = source.Surname;
            destination.Lastname = source.Lastname;
            destination.Number = source.Number;
            destination.City = source.City;
            destination.Rank = source.Rank;
            destination.Year = source.Year;
        }

        public static void UpdateJudge(Judge destination, Judge source)
        {
            destination.Firstname = source.Firstname;
            destination.Surname = source.Surname;
            destination.Lastname = source.Lastname;
            destination.Number = source.Number;
            destination.City = source.City;
            destination.Rank = source.Rank;
            destination.Post = source.Post;
            destination.Info = source.Info;
        }
    }
}
