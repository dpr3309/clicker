using System;
using System.Collections.Generic;

namespace Clicker.Tools.FSMComponents
{
    public class StateMachine<T>
        where T : IState
    {
        protected T _currentState;
        public T CurrentState => _currentState;
        public bool IsRunning => _currentState != null;

        protected Dictionary<Type, T> _states { get; set; }

        public IReadOnlyDictionary<Type, T> States => _states;

        public StateMachine()
        {
            _states = new Dictionary<Type, T>();
        }

        public void Add(T state)
        {
            if (IsRunning)
                throw new InvalidOperationException(
                    $"[StateMachine.Add] {GetType().Name} has already started and cannot receive new states");

            Type type = state.GetType();

            if (_states.ContainsKey(type))
                throw new InvalidOperationException(
                    $"[StateMachine.Add] The given state ({type.Name}) already present in {GetType().Name}");
            _states.Add(type, state);
        }

        public void Event(EventArgs args, params object[] values)
        {
            if (!IsRunning)
                return;

            (Type stateType, object[] values) tuple = _currentState.Event(args, values);

            if (tuple.stateType == (Type)null)
                throw new InvalidOperationException(
                    $"[StateMachine.Event] Returned type of next state is null on event {args.Id} in {CurrentState.GetType().Name}");

            if (tuple.stateType == CurrentState.GetType())
                return;

            ChangeState(tuple.stateType, tuple.values);
        }

        public void Start<TStartState>(params object[] values) where TStartState : T
        {
            if (IsRunning)
                throw new InvalidOperationException($"[StateMachine.Start] {GetType().Name} has already started");

            ChangeState(typeof(TStartState), values);
        }

        private void ChangeState(Type newState, params object[] values)
        {
            if (!_states.ContainsKey(newState))
                throw new InvalidOperationException(
                    $"[StateMachine.ChangeState] The given state ({newState.Name}) was not present in {GetType().Name}");

            _currentState?.OnExit();
            _currentState = _states[newState];
            if (_currentState == null)
                throw new NullReferenceException($"[StateMachine.ChangeState] currentState is null");
            _currentState.Values = values;
            _currentState.OnEnter();
        }
    }
}