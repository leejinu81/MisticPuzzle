using UnityEngine;

namespace Lonely
{
    [SharedBetweenAnimators]
    public class StateMachineEvent : StateMachineBehaviour
    {
        //public delegate void StateMachineEventHandler(Animator animator, int stateMachinePathHash);

        public delegate void StateEventHandler(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

        //public event StateMachineEventHandler OnStateMachineEntered = delegate { };

        //public event StateMachineEventHandler OnStateMachineExited = delegate { };

        public event StateEventHandler OnStateEntered = delegate { };

        public event StateEventHandler OnStateExited = delegate { };

        //public event StateEventHandler OnStateIKed = delegate { };

        //public event StateEventHandler OnStateMoved;

        //public event StateEventHandler OnStateUpdated = delegate { };

        //public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        //{
        //    base.OnStateMachineEnter(animator, stateMachinePathHash);

        //    OnStateMachineEntered(animator, stateMachinePathHash);
        //}

        //public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        //{
        //    base.OnStateMachineExit(animator, stateMachinePathHash);

        //    OnStateMachineExited(animator, stateMachinePathHash);
        //}

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            OnStateEntered(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            OnStateExited(animator, stateInfo, layerIndex);
        }

        //public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    base.OnStateIK(animator, stateInfo, layerIndex);

        //    OnStateIKed(animator, stateInfo, layerIndex);
        //}

        // Jinu : 이 함수를 override하면 animation이 이상해지는 경우가 생김. 원인불명(Unity버그 같음)
        //public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    base.OnStateMove(animator, stateInfo, layerIndex);

        //    if (OnStateMoved.IsValid()) OnStateMoved(animator, stateInfo, layerIndex);
        //}

        //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    base.OnStateUpdate(animator, stateInfo, layerIndex);

        //    OnStateUpdated(animator, stateInfo, layerIndex);
        //}
    }
}