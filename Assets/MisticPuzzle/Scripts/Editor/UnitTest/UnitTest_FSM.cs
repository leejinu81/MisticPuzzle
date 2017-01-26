using Extension;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Zenject;

namespace Lonely.UnitTest
{
    [TestFixture]
    public class UnitTest_FSM
    {
        private PlayerState_Idle _idleState;
        private PlayerState_Move _moveState;

        private List<IFactory<State>> _factories = new List<IFactory<State>>();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // idle
            _idleState = Substitute.For<PlayerState_Idle>(State.Null, State.Null, State.Null);
            var idleFactory = Substitute.For<IFactory<State>>();
            idleFactory.Create().Returns(_idleState);
            _factories.Add(idleFactory);

            // move
            _moveState = Substitute.For<PlayerState_Move>(State.Null, State.Null, State.Null);
            var moveFactory = Substitute.For<IFactory<State>>();
            moveFactory.Create().Returns(_moveState);
            _factories.Add(moveFactory);
        }

        //[SetUp]
        //public void SetUp()
        //{
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //}

        private TestFSM GetFSM()
        {
            var fsm = new TestFSM(_factories);
            var init = fsm as IInitializable;
            if (init.IsValid())
                init.Initialize();

            return fsm;
        }

        [Test]
        public void ChangeState시_새로운_State에_Enter함수를_호출해준다()
        {
            var fsm = GetFSM();
            fsm.ChangeState(_idleState.GetType());

            _idleState.Received().Enter();

            Assert.That(fsm.IsCurrentState(_idleState.GetType()));

            // Change Move State
            fsm.ChangeState(_moveState.GetType());

            _moveState.Received().Enter();

            Assert.That(fsm.IsCurrentState(_moveState.GetType()));
        }

        [Test]
        public void ChangeState시_이전State는_Exit함수를_호출해준다()
        {
            var fsm = GetFSM();
            fsm.ChangeState(_idleState.GetType());
            fsm.ChangeState(_moveState.GetType());

            _idleState.Received().Exit();
        }

        public class TestFSM : FSM<State>
        {
            public TestFSM(List<IFactory<State>> stateFactoryList)
                : base(stateFactoryList, State.Null)
            { }
        }
    }
}