using System;

namespace Clicker.FSMComponents
{
    public interface IState
    {
        object[] Values { get; set; }
        void OnEnter();
        (Type stateType, object[] values) Event(EventArgs args, params object[] values);
        void OnExit();
    }
}