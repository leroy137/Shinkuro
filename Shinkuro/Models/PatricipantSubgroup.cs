using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class PatricipantSubgroup : ModelBase
    {

        private int _number;
        private PatricipantGroup _patricipantGroup;

        public Int32 Number 
        { 
            get { return _number; } 
            set { Set<Int32>(ref _number, value); }
        }

        public PatricipantGroup PatricipantGroup 
        { 
            get { return _patricipantGroup; }
            set { Set<PatricipantGroup>(ref _patricipantGroup, value); }
        }

        public PatricipantSubgroup(PatricipantGroup patricipantGroup)
        {
            PatricipantGroup = patricipantGroup;
        }

        public PatricipantSubgroup(Int32 number, PatricipantGroup patricipantGroup) : this(patricipantGroup)
        {
            Number = number;
        }
    }
}
