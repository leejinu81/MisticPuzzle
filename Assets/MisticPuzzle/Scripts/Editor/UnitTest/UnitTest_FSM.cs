using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Lonely.UnitTest
{
    [TestFixture]
    public class UnitTest_FSM
    {
        private IState _idleState, _moveState;
        private List<IState> _states = new List<IState>();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _idleState = Substitute.For<IState, PlayerState_Idle>(null, null);
            _states.Add(_idleState);

            _moveState = Substitute.For<IState, PlayerState_Move>(null, null, null, 0.0f);
            _states.Add(_moveState);
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        private TestFSM GetFSM() { return new TestFSM(_states); }

        [Test]
        public void 기본State는_NullState이다()
        {
            var fsm = GetFSM();
            var current = fsm.GetCurrent();
            Assert.That(current, Is.SameAs(State.Null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeState시_IState를_파라미터로_넣으면_예외가_발생해야한다()
        {
            var fsm = GetFSM();
            fsm.ChangeState<IState>();
        }

        [Test]
        public void ChangeState시_새로운_State에_Enter함수를_호출해준다()
        {
            var fsm = GetFSM();
            fsm.ChangeState<PlayerState_Idle>();

            _idleState.Received().Enter();

            var current = fsm.GetCurrent();
            Assert.That(current, Is.SameAs(_idleState));
        }

        [Test]
        public void ChangeState시_새로운_State가_current로_바뀐다()
        {
            var fsm = GetFSM();
            fsm.ChangeState<PlayerState_Idle>();

            var current = fsm.GetCurrent();
            Assert.That(current, Is.SameAs(_idleState));
        }

        [Test]
        public void ChangeState시_이전State는_Exit함수를_호출해준다()
        {
            var fsm = GetFSM();
            fsm.ChangeState<PlayerState_Idle>();
            fsm.ChangeState<PlayerState_Move>();

            _idleState.Received().Exit();
        }

        public class TestFSM : FSM
        {
            public TestFSM(List<IState> states) : base(states)
            {
            }

            public void ChangeState<T>()
                where T : class, IState
            {
                ((IFSM)this).ChangeState<T>();
            }

            public IState GetCurrent() { return _curState; }
        }
    }
}