using Shinkuro.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class PatricipantGroup : ModelBase
    {
        private int _number;
        private Patricipant patricipant;

        public Int32 Number
        {
            get { return _number; }
            set { Set<Int32>(ref _number, value); }
        }

        public Patricipant Patricipant
        {
            get { return patricipant; }
            set { Set<Patricipant>(ref patricipant, value); }
        }

        public PatricipantGroup(int number, Patricipant patricipant)
        {
            Number = number;
            Patricipant = patricipant;
        }
    }
}
