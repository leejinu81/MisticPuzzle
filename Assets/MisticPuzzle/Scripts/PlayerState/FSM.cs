using Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public interface IFSM : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        IState curState { get; }
        float stateTime { get; }

        bool IsPrevState<TState>() where TState : class, IState;

        bool IsCurState<TState>() where TState : class, IState;

        void ChangeState<TState>() where TState : class, IState;
    }

    public class FSM : IFSM
    {
        #region Explicit Interface

        IState IFSM.curState { get { return _curState; } }

        float IFSM.stateTime { get { return _stateTime; } }

        bool IFSM.IsPrevState<TState>()
        {
            CheckStateType<TState>();

            return Equals(_prevState.GetType(), typeof(TState));
        }

        bool IFSM.IsCurState<TState>()
        {
            CheckStateType<TState>();

            return Equals(_curState.GetType(), typeof(TState));
        }

        void IFSM.ChangeState<TState>()
        {
            CheckStateType<TState>();

            ChangeState<TState>();
        }

        void IInitializable.Initialize()
        {
            foreach (var iterState in _states)
            {
                var initializeable = iterState as IInitializable;
                if (initializeable.IsValid())
                    initializeable.Initialize();
            }
        }

        void ITickable.Tick()
        {
            _stateTime += Time.deltaTime;

            var tickable = _curState as ITickable;
            if (tickable.IsValid())
                tickable.Tick();
        }

        void IFixedTickable.FixedTick()
        {
            var fixedTickable = _curState as IFixedTickable;
            if (fixedTickable.IsValid())
                fixedTickable.FixedTick();
        }

        void ILateTickable.LateTick()
        {
            var lateTickable = _curState as ILateTickable;
            if (lateTickable.IsValid())
                lateTickable.LateTick();
        }

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

        private readonly List<IState> _states = new List<IState>();

        private IState _prevState = State.Null;
        protected IState _curState = State.Null;
        private float _stateTime;

        public FSM(List<IState> states)
        {
            _states.AddRange(states);
        }

        private void ChangeState<TState>() where TState : class, IState
        {
            TState state = null;
            foreach (var stateIter in _states)
            {
                state = (stateIter as TState);
                if (state.IsValid())
                {
                    _stateTime = 0;
                    _curState.Exit();

                    _prevState = _curState;
                    _curState = state;
                    // 다음 프레임에서 Enter를 호출
                    //yield return null;
                    _curState.Enter();
                    break;
                }
            }

            if (state.IsNull())
                Debug.Log("Not Find " + typeof(TState).Name + " State.");
        }

        private void CheckStateType<TState>() where TState : class, IState
        {
            if (Equals(typeof(TState), typeof(IState)))
                throw new ArgumentOutOfRangeException();
        }

        public class Factory : Factory<List<IState>, FSM>
        {
        }

        #region Null Object

        public static readonly IFSM Null = new NullFSM();

        private class NullFSM : IFSM
        {
            IState IFSM.curState { get { return State.Null; } }

            float IFSM.stateTime { get { return 0; } }

            void IFSM.ChangeState<TState>() { }

            void IDisposable.Dispose() { }

            void IFixedTickable.FixedTick() { }

            void IInitializable.Initialize() { }

            bool IFSM.IsCurState<TState>() { return false; }

            bool IFSM.IsPrevState<TState>() { return false; }

            void ILateTickable.LateTick() { }

            void ITickable.Tick() { }
        }

        #endregion Null Object
    }
}