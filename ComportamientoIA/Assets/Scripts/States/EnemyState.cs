
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;

namespace ComportamientoIA.Runtime.State
{
    public abstract class EnemyState : FiniteStateMachineLibrary.States.State
    {
        protected EnemyBehaviour _controller;

        protected EnemyState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine)
        {
            this._controller = controller;
        }

    }
}
