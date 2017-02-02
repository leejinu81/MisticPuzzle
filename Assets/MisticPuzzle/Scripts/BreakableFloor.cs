using Extension;
using UnityEngine;

namespace Lonely
{
    public class BreakableFloor : MonoBehaviour
    {
        [SerializeField]
        private GameObject _breakFloor = null;

        private Animator _animator;
        private const string BREAK_FLOOR_STATE_NAME = "BreakFloor";
        private StateMachineEventHandler _breakFloorAnimState;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Debug.Assert(_animator.IsValid());
            _breakFloorAnimState = StateMachineEventHandler.New(_animator, BREAK_FLOOR_STATE_NAME);

            Debug.Assert(_breakFloor.IsValid());
            _breakFloor.SetActive(false);
        }

        public void Break()
        {
            _animator.SetTrigger("BreakTrigger");
            _breakFloorAnimState.OnExited += OnBreakAnimEnd;
        }

        private void OnBreakAnimEnd()
        {
            _breakFloorAnimState.OnExited -= OnBreakAnimEnd;
            _breakFloor.SetActive(true);

            _animator.enabled = false;

            var collider = GetComponent<Collider2D>();
            Debug.Assert(collider.IsValid());
            collider.enabled = false;

            var sprite = GetComponent<SpriteRenderer>();
            Debug.Assert(sprite.IsValid());
            sprite.enabled = false;
        }
    }
}