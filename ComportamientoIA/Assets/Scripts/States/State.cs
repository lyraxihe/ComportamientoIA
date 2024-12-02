
using FiniteStateMachineLibrary.Managers;

namespace FiniteStateMachineLibrary.States
{
    public abstract class State
    {
        protected FiniteStateMachine _finiteStateMachine;

        protected State(FiniteStateMachine finiteStateMachine)
        {
            this._finiteStateMachine = finiteStateMachine;
        }

        public abstract void OnEnter();

        public abstract void DoState();

        //public abstract void OnExit();
    }
}
