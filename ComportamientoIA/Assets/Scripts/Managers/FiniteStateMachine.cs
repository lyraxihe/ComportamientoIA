
using FiniteStateMachineLibrary.States;

namespace FiniteStateMachineLibrary.Managers
{
    public abstract class FiniteStateMachine
    {
        private State? _currentState;

        public void UpdateMachine()
        {
            this._currentState?.DoState();
        }

        public State getCurrentState()
        {
            return _currentState;
        }

        public void ChangeToState(State state)
        {
            //this._currentState?.OnExit();
            this._currentState = state;
            this._currentState?.OnEnter();
        }

    }
}
