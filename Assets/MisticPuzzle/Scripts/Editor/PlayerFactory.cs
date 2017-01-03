using Extension;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class PlayerFactory : IFactory<SceneContext, Object, Player>
    {
        #region Explicit Interface

        Player IFactory<SceneContext, Object, Player>.Create(SceneContext sc, Object prefab)
        {
            var playerGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            Debug.Assert(playerGO.IsValid());

            var zb = playerGO.GetComponent<ZenjectBinding>();
            Debug.Assert(zb.IsValid());

            zb.SetContext(sc);

            var player = playerGO.GetComponent<Player>();
            Debug.Assert(player.IsValid());

            return player;
        }

        #endregion Explicit Interface
    }
}