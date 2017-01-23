using Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public abstract class FSM<TState> : IInitializable, ITickable, IDisposable
        where TState : State
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            foreach (var stateFactory in _stateFactoryList)
            {
                var state = stateFactory.Create() as TState;
                _stateDic.Add(state.GetType(), state);
            }
        }

        void ITickable.Tick()
        {
            _stateTime += Time.deltaTime;
            _curState.Update();
        }

        //void IFixedTickable.FixedTick()
        //{
        //}

        //void ILateTickable.LateTick()
        //{
        //}

        #endregion Explicit Interface

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _prevState = null;
                    _curState.Exit();
                    _curState = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FSM() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        // FIXME
        public State curState { get { return _curState; } }

        public float stateTime { get { return _stateTime; } }

        protected readonly List<IFactory<TState>> _stateFactoryList;
        private readonly Dictionary<Type, TState> _stateDic = new Dictionary<Type, TState>();
        private TState _prevState;
        protected TState _curState;

        private float _stateTime;

        public FSM(List<IFactory<TState>> stateFactoryList)
        {
            _stateFactoryList = stateFactoryList;
        }

        public bool IsPrevState<TStateType>()
            where TStateType : State
        {
            return Equals(typeof(TStateType), _prevState.GetType());
        }

        public bool IsCurrentState<TStateType>()
            where TStateType : State
        {
            return Equals(typeof(TStateType), _curState.GetType());
        }

        public void ChangeState<TStateType>()
            where TStateType : State
        {
            TState state = null;
            if (_stateDic.TryGetValue(typeof(TStateType), out state))
            {
                if (_curState.IsValid())
                    _curState.Exit();

                _prevState = _curState;
                _curState = state;
                _curState.Enter();

                _stateTime = 0.0f;
            }

            if (state.IsNull())
                Debug.Log("Not Find " + typeof(TStateType) + " State.");
        }
    }
}