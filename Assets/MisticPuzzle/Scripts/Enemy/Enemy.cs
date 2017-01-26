using UnityEngine;
using Zenject;

namespace Lonely
{
    public abstract class Enemy : IInitializable
    {
        void IInitializable.Initialize()
        {
            Initialize();
        }

        public abstract bool hasTitanSheild { get; }

        public Enemy()
        {
        }

        protected virtual void Initialize()
        { }

        public abstract void Die();

        public abstract void Turn();
    }    

    public class TargetPositionFactory : Factory<GameObject>
    {
    }
}