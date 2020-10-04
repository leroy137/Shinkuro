using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Shinkuro.Services.Navigation.Interfaces
{
    public interface IPageResolver
    {
        Page GetPageInstance(string alias);
    }
}
