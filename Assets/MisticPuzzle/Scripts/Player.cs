using Extension;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lonely
{
    public class Player : MonoBehaviour
    {
        private IFSM _fsm;
        private Text _escapeText;        

        private void Start()
        {
            _fsm.ChangeState<PlayerState_Idle>();
            _escapeText.enabled = false;            
        }

        [Inject]
        public void Inject(IFSM fsm, SceneContext sc, [Inject(Id = "escape")]Text escapeText)
        {
            _fsm = fsm;
            _escapeText = escapeText;            

            var zb = GetComponent<ZenjectBinding>();
            Debug.Assert(zb.IsValid());

            zb.SetContext(sc);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Equals(collision.tag, "Exit"))
            {
                _escapeText.enabled = true;
            }
        }        
    }
}