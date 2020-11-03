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
        public ObservableCollection<Group> Groups { get; set; }
        public ApplicationCoreContext()
        {
            CurrentCompetition = null;
            Patricipants = new ObservableCollection<Patricipant>();
            Judges = new ObservableCollection<Judge>();
            Groups = new ObservableCollection<Group>();
        }

        public void UpdateCompetition(Competition competition)
        {
            CurrentCompetition.UpdateCompetition(competition);
        }

        public void UpdatePatricipant(Patricipant destination, Patricipant source)
        {
            if (destination == null)
                throw new Exception("Изменения учатника невозможно, так как он не выбран и равен null!");

            if (source == null)
                throw new Exception("Изменения участника невозможно, так как измененный обект участника не выбран и равен null!");

            destination.Surname = source.Surname;
            destination.Name = source.Name;
            destination.Patronymic = source.Patronymic;
            destination.Number = source.Number;
            destination.City = source.City;
            destination.Rank = source.Rank;
            destination.Year = source.Year;
        }

        public void UpdateJudge(Judge destination, Judge source)
        {
            if (destination == null)
                throw new Exception("Изменения судьи невозможно, так как он не выбран и равен null!");

            if (source == null)
                throw new Exception("Изменения судьи невозможно, так как измененный обект судьи не выбран и равен null!");

            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.Patronymic = source.Patronymic;
            destination.Number = source.Number;
            destination.City = source.City;
            destination.Rank = source.Rank;
            destination.Post = source.Post;
            destination.Info = source.Info;
        }

        public void UpdateGroup(Group destination, Group source)
        {
            if (destination == null)
                throw new Exception("Изменение группы невозможно, так как она не выбрана и равна null!");

            if (source == null)
                throw new Exception("Изменение группы невозможно, так как измененный обект группы не выбран и равен null!");

            destination.Name = source.Name;
            destination.StartYear = source.StartYear;
            destination.FinishYear = source.FinishYear;
            destination.Description = source.Description;
        }

        public void AddPatricipant(Patricipant patricipant)
        {
            if (patricipant == null)
                throw new NullReferenceException("Участник не задан!");

            Patricipants.Add(patricipant);
        }

        public void AddJudge(Judge judge)
        {
            if (judge == null)
                throw new NullReferenceException("Судья не задан!");

            Judges.Add(judge);
        }

        public void AddGroup(Group group)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задан!");

            Groups.Add(group);
        }

        public bool RemovePatricipant(Patricipant patricipant)
        {
            if (patricipant == null)
                throw new NullReferenceException("Участник не задан!");

            return Patricipants.Remove(patricipant);
        }

        public bool RemoveJudge(Judge judge)
        {
            if (judge == null)
                throw new NullReferenceException("Судья не задан!");

            return Judges.Remove(judge);
        }

        public bool RemoveGroup(Group group)
        {
            if (group == null)
                throw new NullReferenceException("Группа для удаления не задана!");

            return Groups.Remove(group);
        }
    }
}
