using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class JudgeGroup : ModelBase
    {
        private int _number;
        private Judge _judge;

        public Int32 Number 
        { 
            get { return _number; } 
            set { Set<Int32>(ref _number, value); } 
        }

        public Judge Judge 
        { 
            get { return _judge; } 
            set { Set<Judge>(ref _judge, value); }
        }

        public JudgeGroup(int number, Judge judge)
        {
            Number = number;
            Judge = judge;
        }
    }
}
