using System;
using System.Collections.Generic;
using Clicker.FSMComponents;
using EventArgs = Clicker.FSMComponents.EventArgs;

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