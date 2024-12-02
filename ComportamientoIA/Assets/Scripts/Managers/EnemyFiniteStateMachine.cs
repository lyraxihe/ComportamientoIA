
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.State;

namespace ComportamientoIA.Runtime.Managers
{
    public sealed class EnemyFiniteStateMachine : FiniteStateMachine
    {
        private EnemyBehaviour _controller;

        public EnemyFiniteStateMachine(EnemyBehaviour controller)
        {
            _controller = controller;
            ChangeToState(new PatrolState(this, this._controller));
        }
    }
}
