using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTYD.ObserverModel;

namespace KTYD.Model
{
    /// <summary>
    /// Observable object (Object being observed)
    /// 
    /// Observer pattern
    /// </summary>
    public class Observable
    {

        protected List<KTYD.ObserverModel.Observer> container;   // List of all observers
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Observable()
        {
            this.container = new List<KTYD.ObserverModel.Observer>();
        }

        /// <summary>
        /// Register observer
        /// </summary>
        public void register(KTYD.ObserverModel.Observer obj)
        {
            this.container.Add(obj);
        }

        /// <summary>
        /// Remove observer
        /// </summary>
        public void unregister(KTYD.ObserverModel.Observer obj)
        {
            if (this.container.Contains(obj))
            {
                this.container.Remove(obj);
            }
        }

        /// <summary>
        /// Notify observers for any change
        /// </summary>
        public void notify()
        {
            foreach (KTYD.ObserverModel.Observer e in container)
            {
                e.update();
            }
        }
    }
}
