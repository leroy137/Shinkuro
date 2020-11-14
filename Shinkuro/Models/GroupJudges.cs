using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinkuro.Models
{
    public class GroupJudges : ModelBase
    {
        private String _name;

        public String Name 
        { 
            get { return _name; } 
            set 
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Название бригады судей не может быть пустым!");

                Set<String>(ref _name, value); 
            }
        }

        public String JudgesListText => Judges.Count>0? String.Join(";   ", Judges.Select(j=>j.Judge.ShortFIO)) : "";

        public String JudgesNameListText => $"Название: {Name} - (" + (Judges.Count > 0 ? String.Join(";   ", Judges.Select(j => j.Judge.ShortFIO)) : "список судей пуст") + ")";

        public List<JudgeGroup> Judges { get; set; } = new List<JudgeGroup>();

        public GroupJudges(String name)
        {
            Name = name;
        }

        public GroupJudges(String name, List<Judge> judges) : this(name)
        {
            if (judges.Count == 0)
                throw new Exception("Количество судей в бригаде не можут быть равным 0");

            for(int i=0;i<judges.Count;i++)
            {
                var current = judges[i];
                for(int j=i+1;j<judges.Count;j++)
                {
                    if (current == judges[j])
                        throw new Exception("В писке судей имеются дубли!");
                }
                Judges.Add(new JudgeGroup(i + 1, current));
            }
        }
    }
}
