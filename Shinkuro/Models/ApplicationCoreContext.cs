using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Shinkuro.Services;

namespace Shinkuro.Models
{
    internal class ApplicationCoreContext
    {
        public Competition CurrentCompetition { get; set; }

        public static ObservableCollection<Figure> Figures { get; set; }
        public ObservableCollection<Patricipant> Patricipants { get; set; }
        public ObservableCollection<Judge> Judges { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ApplicationCoreContext()
        {
            CurrentCompetition = null;
            Patricipants = new ObservableCollection<Patricipant>();
            Judges = new ObservableCollection<Judge>();
            Groups = new ObservableCollection<Group>();

            for (int i = 0; i < 10; i += 2)
                Groups.Add(new Group($"Группа {i + 1}", 2000 + i, 2001 + i, $"Описание группы {i + 1}"));

            for (int i = 0; i < 50; i++)
                Patricipants.Add(Patricipant.CreateRandom());


            AutoFillGroups();
        }

        static ApplicationCoreContext()
        {
            Figures = new ObservableCollection<Figure>();


            for(int i=0;i<10;i++)
            {
                Figures.Add(new Figure($"Фигура {i}", 1.2+i, $"Тестовое описание {i}"));
            }
        }

        public void UpdateCompetition(Competition competition)
        {

            String logopath = competition.LogoPath;
            using (FileStream s = File.Open(logopath, FileMode.Open))
            {

                if (s.Length > FileManager.MaxSizeLogo)
                {
                    s.Dispose();
                    throw new Exception($"Размер логотипа превышает {(FileManager.MaxSizeLogo / 1024.0 / 1024.0)}МБ.");
                }
            }

            String filelogoUploadPath = FileManager.UploadLogoCompetition(logopath);

            if (String.IsNullOrEmpty(filelogoUploadPath))
                throw new Exception("Не удалось загрузить логотип!");

            competition.LogoPath = filelogoUploadPath;
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
            destination.City = source.City;
            destination.Rank = source.Rank;
            destination.Year = source.Year;
            destination.SportSchool = source.SportSchool;
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

        public static void UpdateFigure(Figure destination, Figure source)
        {
            destination.Name = source.Name;
            destination.Complexity = source.Complexity;
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

        public static void AddFigure(Figure figure)
        {
            if (figure == null)
                throw new NullReferenceException("Добавление фигуры невозможно, так как обеъкт фигуры не задан и равен null");

            Figures.Add(figure);
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

        public static bool RemoveFigure(Figure figure)
        {
            if (figure == null)
                throw new NullReferenceException("Удаление фигуры невозможно, так как обеъкт фигуры не задан и равен null");

            return Figures.Remove(figure);
        }


        public void SelectFiguresGroup(Group group, List<Figure> selected)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задана для заполнения фигур, пожалуйста выберите группу или обновите список!");

            group.Figures.Clear();
            foreach (var figure in selected)
                group.Figures.Add(figure);
        }

        public bool UnsetGroupFigure(Group group, Figure figure)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задана для открепления фигуры, пожалуйста выберите группу или обновите список!");

            return group.Figures.Remove(figure);
        }


        /// <summary>
        /// метод автоматического заполнения групп
        /// </summary>
        public void AutoFillGroups()
        {
            List<Patricipant> patricipantsWithoutGroup = new List<Patricipant>();

            foreach(Patricipant patricipant in Patricipants)
            {
                bool isDetermined = false;
                foreach(Group g in Groups)
                {
                    if (g.IsSatisfiedPatricipant(patricipant))
                    {
                        isDetermined = true;
                        AddPatricipantGroup(g, patricipant);
                        break;
                    }
                }

                if(!isDetermined)
                {
                    patricipantsWithoutGroup.Add(patricipant);
                }
            }
        }


        public void AddPatricipantGroup(Group group, Patricipant p)
        {
            group.Patricipants.Add(p);
        }
    }
}
