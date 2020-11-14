using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class GroupJudgesFigure : ModelBase
    {
        private int _number;
        private Figure _figure;
        private GroupJudges _groupJudges;

        public int Number 
        { 
            get { return _number; }
            set { Set<Int32>(ref _number, value); }
        }

        public Figure Figure 
        { 
            get { return _figure; }
            set { Set<Figure>(ref _figure, value); }
        }

        public GroupJudges GroupJudges 
        { 
            get { return _groupJudges; } 
            set { Set<GroupJudges>(ref _groupJudges, value); }
        }

        public GroupJudgesFigure(int number, Figure figure, GroupJudges groupJudges)
        {
            Number = number;
            Figure = figure;
            GroupJudges = groupJudges;
        }
    }
}
