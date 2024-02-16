using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Tools.FSMComponents
{
    public abstract class AState : IState
    {
        public static readonly EventArgs Click = new EventArgs("Click");
        public static readonly EventArgs Update = new EventArgs("Update");

        protected abstract Dictionary<EventArgs, Type> HandledEvents { get; }

        public object[] Values { get; set; }

        public virtual void OnEnter()
        {
            LogStateName();
        }

        protected virtual void LogStateName()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual (Type stateType, object[] values) Event(EventArgs args, params object[] values)
        {
            if (HandledEvents != null && HandledEvents.ContainsKey(args))
                return (HandledEvents[args], values);

            if (HandledEvents == null)
            {
                Debug.LogWarning($"in state {GetType().Name}, HandledEvents is null!");
            }

            return (GetType(), values);
        }
    }
}