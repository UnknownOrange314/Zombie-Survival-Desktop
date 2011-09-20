using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.ObserverModel
{
    /// <summary>
    /// Observer interface
    /// </summary>
    public interface Observer
    {
        /// <summary>
        /// Calling update in the class that is based on this interface
        /// </summary>
        void update();
    }
}
