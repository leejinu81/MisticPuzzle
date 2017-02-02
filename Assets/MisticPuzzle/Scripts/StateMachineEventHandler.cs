using Extension;
using System;
using UnityEngine;

namespace Lonely
{
    public class StateMachineEventHandler : IDisposable
    {
        public event Action OnEntered
        {
            add { _onStateEntered += value; }
            remove { _onStateEntered -= value; }
        }

        public event Action OnExited
        {
            add { _onStateExited += value; }
            remove { _onStateExited -= value; }
        }

        #region IDisposable Impl

        private bool _disposed;

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //TODO: Managed cleanup code here, while managed refs still valid
                _stateMachineEvent.OnStateEntered -= StateEntered;
                _stateMachineEvent.OnStateExited -= StateExited;

                _onStateEntered = null;
                _onStateExited = null;
            }
            //TODO: Unmanaged cleanup code here

            _disposed = true;
        }

        #endregion IDisposable Impl

        private readonly StateMachineEvent _stateMachineEvent;
        private readonly Animator _animator;
        private readonly string _stateName;
        private readonly int _layerIndex;

        private Action _onStateEntered = delegate { };
        private Action _onStateExited = delegate { };

        public static StateMachineEventHandler New(Animator animator, string stateName, int layerIndex = 0)
        {
            return new StateMachineEventHandler(animator, stateName, layerIndex);
        }

        protected StateMachineEventHandler(Animator animator, string stateName, int layerIndex = 0)
        {
            _animator = animator;
            Debug.Assert(_animator.IsValid());

            _stateMachineEvent = _animator.GetBehaviour<StateMachineEvent>();
            Debug.Assert(_stateMachineEvent.IsValid());

            _stateName = stateName;
            Debug.Assert(string.IsNullOrEmpty(_stateName).IsFalse());

            _layerIndex = layerIndex;

            _stateMachineEvent.OnStateEntered += StateEntered;
            _stateMachineEvent.OnStateExited += StateExited;
        }

        private void StateEntered(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsMyState(animator, stateInfo, layerIndex))
                _onStateEntered();
        }

        private void StateExited(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsMyState(animator, stateInfo, layerIndex))
                _onStateExited();
        }

        private bool IsMyState(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            return Equals(_animator, animator) && stateInfo.IsName(_stateName) && Equals(_layerIndex, layerIndex);
        }
    }
}