using System;

namespace Game.Mechanics
{
    public interface IDamageable
    {
        /// <summary>
        /// A method that processes an event caused as a result of damage.
        /// </summary>
        public void GetDamage();
    }
}

