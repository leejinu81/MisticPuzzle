using UnityEngine;
using Zenject;

namespace Lonely
{
    public class MisticPuzzleInput : ITickable
    {
        #region Explicit Interface

        void ITickable.Tick()
        {
            _horizontal = (int)Input.GetAxisRaw("Horizontal");
            _vertical = (int)Input.GetAxisRaw("Vertical");            
        }

        #endregion Explicit Interface

        public int horizontal { get { return _horizontal; } }
        public int vertical { get { return _vertical; } }

        private int _horizontal, _vertical;
    }
}