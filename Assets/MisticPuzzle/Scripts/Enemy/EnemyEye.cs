using Extension;
using UnityEngine;

namespace Lonely
{
    public class EnemyEye
    {
        private readonly EnemyModel _model;
        private readonly LayerMask _blockLayer;

        public EnemyEye(EnemyModel model, LayerMask blockLayer)
        {
            _model = model;
            _blockLayer = blockLayer;
        }

        /// <summary>
        /// 전방에 Player가 있는지 본다
        /// </summary>
        /// <param name="player">찾은 Player</param>
        /// <returns>Player를 찾았으면 true</returns>
        public bool Look(out Player player)
        {
            player = null;
            var hitInfo = Raycast(_model.position, _model.dir);
            if (IsPlayer(hitInfo))
            {
                player = hitInfo.transform.GetComponent<Player>();
                Debug.Assert(player.IsValid());

                return true;
            }
            return false;
        }

        private RaycastHit2D Raycast(Vector2 start, Vector2 end)
        {
            _model.enableCollider = false;
            var hitInfo = Physics2D.Raycast(start, end, 1000, _blockLayer);
            _model.enableCollider = true;

            return hitInfo;
        }

        private bool IsPlayer(RaycastHit2D hitInfo)
        {
            return hitInfo.transform.IsValid() && hitInfo.transform.CompareTag("Player");
        }
    }
}