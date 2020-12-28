using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Shinkuro.Services;

namespace Shinkuro.Models
{
    public class ApplicationCoreContext
    {
        public Competition CurrentCompetition { get; set; }

        public static ObservableCollection<Figure> Figures { get; set; }
        public ObservableCollection<Patricipant> Patricipants { get; set; }
        public ObservableCollection<Judge> Judges { get; set; }
        public ObservableCollection<AgeCategory> Groups { get; set; }

        public ObservableCollection<GroupJudges> GroupJudges { get; set; } = new ObservableCollection<GroupJudges>();
        public ApplicationCoreContext()
        {
            CurrentCompetition = null;
            Patricipants = new ObservableCollection<Patricipant>();
            Judges = new ObservableCollection<Judge>();
            Groups = new ObservableCollection<AgeCategory>();
            GroupJudges = new ObservableCollection<GroupJudges>();

            for (int i = 0; i < 10; i += 2)
                Groups.Add(new AgeCategory($"Группа {i + 1}", 2000 + i, 2001 + i, $"Описание группы {i + 1}"));

            for (int i = 0; i < 150; i++)
                Patricipants.Add(Patricipant.CreateRandom());

            for (int i = 0; i <25; i++)
                Judges.Add(Judge.CreateRandom());

            GenerateGroupJudges();
        }

        private void GenerateGroupJudges()
        {
            Random rand = new Random();

            int maxCountJudgeInGroup = 6;
            int maxCountGroupJudges = 4;
            for(int i=0;i<maxCountGroupJudges;i++)
            {
                String name = $"Бригада {i + 1}";
                List<Judge> judges = new List<Judge>();

                while(judges.Count!= maxCountJudgeInGroup)
                {
                    Judge nextJudge = Judges[rand.Next(Judges.Count)];
                    if (!judges.Contains(nextJudge))
                        judges.Add(nextJudge);
                }

                GroupJudges groupJudges = new GroupJudges(name, judges);
                GroupJudges.Add(groupJudges);
            }
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
            if (!String.IsNullOrEmpty(logopath)&&File.Exists(logopath))
            {
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
            }

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
            destination.Coach = source.Coach;
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

        public void UpdateGroup(AgeCategory destination, AgeCategory source)
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

        public void AddGroup(AgeCategory group)
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

        public void AddGroupJudges(GroupJudges g)
        {
            if (g == null)
                throw new NullReferenceException("Добавление бригады невозможно, так как объект бригады судей не задан и равен null");

            GroupJudges.Add(g);
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

        public bool RemoveGroup(AgeCategory group)
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

        public bool RemoveGroupJudges(GroupJudges group)
        {
            if (group == null)
                throw new NullReferenceException("Удаление бригады судей невозможно, так как обеъкт бригады не задан и равен null");

            return GroupJudges.Remove(group);
        }


        public void SelectFiguresGroup(AgeCategory group, List<Figure> selected)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задана для заполнения фигур, пожалуйста выберите группу или обновите список!");

            group.Figures.Clear();
            int i = 1;
            foreach (var figure in selected)
                group.Figures.Add(new FigureAgeCategory(i++,figure));
        }

        public bool UnsetGroupFigure(AgeCategory group, FigureAgeCategory figure)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задана для открепления фигуры, пожалуйста выберите группу или обновите список!");

            int index = group.Figures.FindIndex(f => f == figure);
            if (index < 0)
                throw new Exception($"Фигура {figure.Figure.Name} для открепления не найдена в списке фигур группы!");

            if(group.Figures.Remove(figure))
            {
                for(int i=index;i<group.Figures.Count;i++)
                {
                    group.Figures[i].Number -= 1;
                }
                return true;
            }
            return false;
        }

        public bool UnsetGroupPatricipant(AgeCategory group, PatricipantGroup p)
        {
            if (group == null)
                throw new NullReferenceException("Группа не задана для открепления участника, пожалуйста выберите группу или обновите список!");

            int index = group.Patricipants.FindIndex(item => item == p);
            if (index < 0)
                throw new Exception($"Участник {p.Patricipant.FIO} для открепления не найден в списке участников группы!"); ;

            if (group.Patricipants.Remove(p))
            {
                for (int i = index; i < group.Patricipants.Count; i++)
                {
                    group.Patricipants[i].Number -= 1;
                }
                return true;
            }
            return false;
        }


        public bool UnsetGroupJudgesFigure(AgeCategory group, GroupJudgesFigure groupJudgesFigure)
        {
            if (group == null)
                throw new Exception("Группа для открепления фигуры и судей не выбрана и равна null!");

            if (groupJudgesFigure == null)
                throw new Exception("Объект фигура и судьи для оценивания не заданы и равен null!");

            bool res = group.GroupJudgesFiguresList.Remove(groupJudgesFigure);

            if (res)
            {
                for (int i = 0; i < group.GroupJudgesFiguresList.Count; i++)
                {
                    group.GroupJudgesFiguresList[i].Number = i + 1;
                }
            }

            return res;
        }


        /// <summary>
        /// метод автоматического заполнения групп
        /// </summary>
        public void AutoFillGroups()
        {
            List<Patricipant> patricipantsWithoutGroup = new List<Patricipant>();
            foreach(AgeCategory g in Groups)
            {
                g.Patricipants = new List<PatricipantGroup>();
            }

            foreach(Patricipant patricipant in Patricipants)
            {
                bool isDetermined = false;
                foreach(AgeCategory g in Groups)
                {
                    if (g.IsSatisfiedPatricipant(patricipant))
                    {
                        isDetermined = true;
                        AddPatricipantGroup(g, new PatricipantGroup(1,patricipant));
                        break;
                    }
                }

                if(!isDetermined)
                {
                    patricipantsWithoutGroup.Add(patricipant);
                }
            }

            foreach (AgeCategory g in Groups)
            {
                g.Patricipants.Sort((p1,p2)=>p1.Patricipant.FIO.CompareTo(p2.Patricipant.FIO));
            }

            foreach(AgeCategory g in Groups)
            {
                for(int i=0;i<g.Patricipants.Count;i++)
                {
                    g.Patricipants[i].Number = i + 1;
                }
            }
        }


        public void AddPatricipantGroup(AgeCategory group, PatricipantGroup p)
        {
            group.Patricipants.Add(p);
            for(int i=0;i<group.Patricipants.Count;i++)
            {
                group.Patricipants[i].Number = i + 1;
            }
        }

        /// <summary>
        /// Добавление в группу объекта фигура - список судей
        /// </summary>
        /// <param name="group">Группа участников куда прикрепляется фигура и список судей</param>
        /// <param name="groupJudgesFigure">Объект фигура-судьи</param>
        public void AddGroupJudgesFigure(AgeCategory group, GroupJudgesFigure groupJudgesFigure)
        {
            if (group == null)
                throw new Exception("Группа для добавления фигуры и судей не выбрана и равна null!");

            if (groupJudgesFigure == null)
                throw new Exception("Объект фигура и судьи для оценивания не заданы и равен null!");

            groupJudgesFigure.Number = group.GroupJudgesFiguresList.Count + 1;
            group.GroupJudgesFiguresList.Add(groupJudgesFigure);
        }
    }
}
