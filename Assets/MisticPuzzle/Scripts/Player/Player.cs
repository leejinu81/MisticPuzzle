using Extension;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Lonely
{
    public class Player : MonoBehaviour
    {
        private PlayerFSM _fsm;
        private PlayerModel _model;
        private GameCommands.Escape _escapeCommand;

        private void Start()
        {
            _fsm.ChangeState<PlayerState_Idle>();
        }

        [Inject]
        public void Inject(PlayerFSM fsm, PlayerModel model, SceneContext sc, GameCommands.Escape escapeCommand)
        {
            _fsm = fsm;
            _model = model;
            _escapeCommand = escapeCommand;

            var zb = GetComponent<ZenjectBinding>();
            Debug.Assert(zb.IsValid());

            zb.SetContext(sc);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Exit"))
            {
                _escapeCommand.Execute();

                // Next Scene
                var nextSceneIdx = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneIdx.IsLess(SceneManager.sceneCountInBuildSettings))
                    SceneManager.LoadScene(nextSceneIdx);
            }
            else if (collision.CompareTag("HalfBreakFloor"))
            {                
                _model.stepOnFloor = collision;
            }
        }

        public void PlayerTurn()
        {
            _fsm.PlayerTurn();
        }

        public void Die()
        {
            _fsm.ChangeState<PlayerState_Die>();
        }
    }
}