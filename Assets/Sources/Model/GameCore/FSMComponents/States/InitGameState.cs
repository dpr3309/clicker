using System;
using System.Collections.Generic;
using Clicker.Tools.FSMComponents;
using EventArgs = Clicker.Tools.FSMComponents.EventArgs;

namespace Clicker.Model.FSMComponents.States
{
    internal class InitGameState : AState
    {
        public static EventArgs StartupEvent = new EventArgs("Startup");

        protected override Dictionary<EventArgs, Type> HandledEvents { get; } = new Dictionary<EventArgs, Type>()
        {
            [StartupEvent] = typeof(ReadyToStartState)
        };
    }
}