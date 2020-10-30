using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    internal class ApplicationCoreContext
    {
        public Competition CurrentCompetition { get; set; }

        public ApplicationCoreContext()
        {
            CurrentCompetition = null;
        }
    }
}
