using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Database.Interfaces
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
